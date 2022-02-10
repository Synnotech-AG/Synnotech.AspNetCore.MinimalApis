using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace Synnotech.AspNetCore.MinimalApis.Responses.Internals;

// TODO: input summary & comments from dotnet-class
public class FileResponseHelper
{
    private const string AcceptedRangeHeaderValue = "bytes";

    internal static async Task WriteFileAsync(HttpContext context, Stream fileStream, RangeItemHeaderValue? range, long rangeLength)
    {
        const int bufferSize = 64 * 1024;
        var outputStream = context.Response.Body;
        await using (fileStream)
        {
            try
            {
                if (range == null)
                {
                    await StreamCopyOperation.CopyToAsync(fileStream, outputStream, count: null, bufferSize, cancel: context.RequestAborted);
                }
                else
                {
                    fileStream.Seek(range.From!.Value, SeekOrigin.Begin);
                    await StreamCopyOperation.CopyToAsync(fileStream, outputStream, rangeLength, bufferSize, context.RequestAborted);
                }
            }
            catch (OperationCanceledException)
            {
                // Don't throw this exception, it's most likely caused by the client disconnecting.
                // However, if it was cancelled for any other reason we need to prevent empty responses.
                context.Abort();
            }
        }
    }

    internal static async Task WriteFileAsync(HttpContext context, ReadOnlyMemory<byte> buffer, RangeItemHeaderValue? range, long rangeLength)
    {
        var outputStream = context.Response.Body;

        try
        {
            if (range is null)
            {
                await outputStream.WriteAsync(buffer, context.RequestAborted);
            }
            else
            {
                var from = 0;
                var length = 0;

                checked
                {
                    // Overflow should throw
                    from = (int) range.From!.Value;
                    length = (int) rangeLength;
                }

                await outputStream.WriteAsync(buffer.Slice(from, length), context.RequestAborted);
            }
        }
        catch (OperationCanceledException)
        {
            // Don't throw this exception, it's most likely caused by the client disconnecting.
            // However, if it was cancelled for any other reason we need to prevent empty responses.
            context.Abort();
        }
    }

    internal static (RangeItemHeaderValue? range, long rangeLength, bool serveBody) SetHeaders(
        HttpContext httpContext,
        in FileResponseInfo result,
        long? fileLength,
        bool enableRangeProcessing,
        DateTimeOffset? lastModified,
        EntityTagHeaderValue? etag)
    {
        var request = httpContext.Request;
        var httpRequestHeaders = request.GetTypedHeaders();

        if (lastModified.HasValue)
        {
            lastModified = RoundDownToWholeSeconds(lastModified.Value);
        }

        var preconditionState = GetPreconditionState(httpRequestHeaders, lastModified, etag);

        var response = httpContext.Response;
        SetLastModifiedAndEtagHeaders(response, lastModified, etag);

        // Short circuit if the pre conditional headers process to 304(NotModified) or 412(PreconditionFailed)
        if (preconditionState == PreconditionState.NotModified)
        {
            response.StatusCode = StatusCodes.Status304NotModified;
            return (range: null, rangeLength: 0, serveBody: false);
        }
        else if (preconditionState == PreconditionState.PreconditionFailed)
        {
            response.StatusCode = StatusCodes.Status412PreconditionFailed;
            return (range: null, rangeLength: 0, serveBody: false);
        }

        response.ContentType = result.ContentType;
        SetContentDispositionHeader(httpContext, in result);

        if (fileLength.HasValue)
        {
            // Assuming the request is not a range request, and the response body is not empty, the Content-Length header is set to
            // the length of the entire file.
            // If the request is a valid range request, this header is overwritten with the length of the range as part of the
            // range processing (see method SetContentLength).

            response.ContentLength = fileLength.Value;

            // Handle range request
            if (enableRangeProcessing)
            {
                SetAcceptRangeHeader(response);

                // If the request method is HEAD or GET, PreconditionState is Unspecified or ShouldProcess, and IfRange header is valid,
                // range should be processed and Range headers should be set
                if ((HttpMethods.IsHead(request.Method) || HttpMethods.IsGet(request.Method))
                    && (preconditionState == PreconditionState.Unspecified || preconditionState == PreconditionState.ShouldProcess)
                    && (IfRangeValid(httpRequestHeaders, lastModified, etag)))
                {
                    return SetRangeHeaders(httpContext, httpRequestHeaders, fileLength.Value);
                }
            }
        }

        return (range: null, rangeLength: 0, serveBody: !HttpMethods.IsHead(request.Method));
    }

    internal static bool IfRangeValid(
        RequestHeaders httpRequestHeaders,
        DateTimeOffset? lastModified,
        EntityTagHeaderValue? etag)
    {
        var ifRange = httpRequestHeaders.IfRange;
        if (ifRange != null)
        {
            if (ifRange.LastModified.HasValue)
            {
                if (lastModified.HasValue && lastModified > ifRange.LastModified)
                {
                    return false;
                }
            }
            else if (etag != null && ifRange.EntityTag != null && !ifRange.EntityTag.Compare(etag, useStrongComparison: true))
            {
                return false;
            }
        }

        return true;
    }

    internal static PreconditionState GetPreconditionState(
        RequestHeaders httpRequestHeaders,
        DateTimeOffset? lastModified,
        EntityTagHeaderValue? etag)
    {
        var ifMatchState = PreconditionState.Unspecified;
        var ifNoneMatchState = PreconditionState.Unspecified;
        var ifModifiedSinceState = PreconditionState.Unspecified;
        var ifUnmodifiedSinceState = PreconditionState.Unspecified;

        var ifMatch = httpRequestHeaders.IfMatch;
        if (etag != null)
        {
            ifMatchState = GetEtagMatchState(
                useStrongComparison: true,
                etagHeader: ifMatch,
                etag: etag,
                matchFoundState: PreconditionState.ShouldProcess,
                matchNotFoundState: PreconditionState.PreconditionFailed);
        }

        var ifNoneMatch = httpRequestHeaders.IfNoneMatch;
        if (etag != null)
        {
            ifNoneMatchState = GetEtagMatchState(
                useStrongComparison: false,
                etagHeader: ifNoneMatch,
                etag: etag,
                matchFoundState: PreconditionState.NotModified,
                matchNotFoundState: PreconditionState.ShouldProcess);
        }

        var now = RoundDownToWholeSeconds(DateTimeOffset.UtcNow);

        var ifModifiedSince = httpRequestHeaders.IfModifiedSince;
        if (lastModified.HasValue && ifModifiedSince <= now)
        {
            var modified = ifModifiedSince < lastModified;
            ifModifiedSinceState = modified ? PreconditionState.ShouldProcess : PreconditionState.NotModified;
        }

        var ifUnmodifiedSince = httpRequestHeaders.IfModifiedSince;
        if (lastModified.HasValue && ifUnmodifiedSince <= now)
        {
            var unmodified = ifUnmodifiedSince >= lastModified;
            ifModifiedSinceState = unmodified ? PreconditionState.ShouldProcess : PreconditionState.PreconditionFailed;
        }

        return GetMaxPreconditionState(ifMatchState, ifNoneMatchState, ifModifiedSinceState, ifUnmodifiedSinceState);
    }

    private static PreconditionState GetEtagMatchState(
        bool useStrongComparison,
        IList<EntityTagHeaderValue> etagHeader,
        EntityTagHeaderValue etag,
        PreconditionState matchFoundState,
        PreconditionState matchNotFoundState)
    {
        if (etagHeader?.Count > 0)
        {
            var state = matchFoundState;
            foreach (var entityTag in etagHeader)
            {
                if (entityTag.Equals(EntityTagHeaderValue.Any) || entityTag.Compare(etag, useStrongComparison))
                {
                    state = matchFoundState;
                    break;
                }
            }

            return state;
        }

        return PreconditionState.Unspecified;
    }

    private static (RangeItemHeaderValue? range, long rangeLength, bool serveBody) SetRangeHeaders(
        HttpContext httpContext,
        RequestHeaders httpRequestHeaders,
        long fileLength)
    {
        var response = httpContext.Response;
        var httpResponseHeaders = response.GetTypedHeaders();
        var serveBody = !HttpMethods.IsHead(httpContext.Request.Method);

        // Range may be null for empty range header, invalid ranges, parsing errors, multiple ranges
        // and when the file length is zero.
        var (isRangeRequest, range) = RangeHelper.ParseRange(
            httpContext,
            httpRequestHeaders,
            fileLength);

        if (!isRangeRequest)
        {
            return (range: null, rangeLength: 0, serveBody);
        }

        // Requested range is not satisfiable
        if (range == null)
        {
            // 14.16 Content-Range - A server sending a response with status code 416 (Requested range not satisfiable)
            // SHOULD include a Content-Range field with a byte-range-resp-spec of "*". The instance-length specifies
            // the current length of the selected resource.  e.g. */length
            response.StatusCode = StatusCodes.Status416RangeNotSatisfiable;
            httpResponseHeaders.ContentRange = new ContentRangeHeaderValue(fileLength);
            response.ContentLength = 0;

            return (range: null, rangeLength: 0, serveBody: false);
        }

        response.StatusCode = StatusCodes.Status206PartialContent;
        httpResponseHeaders.ContentRange = new ContentRangeHeaderValue(
            range.From!.Value,
            range.To!.Value,
            fileLength);

        // Overwrite the Content-Length header for valid range requests with the range length.
        var rangeLength = SetContentLength(response, range);

        return (range, rangeLength, serveBody);
    }

    private static long SetContentLength(HttpResponse response, RangeItemHeaderValue range)
    {
        var start = range.From!.Value;
        var end = range.To!.Value;
        var length = end - start + 1;
        response.ContentLength = length;
        return length;
    }

    private static void SetContentDispositionHeader(HttpContext httpContext, in FileResponseInfo result)
    {
        if (!string.IsNullOrEmpty(result.FileDownloadName))
        {
            var contentDisposition = new ContentDispositionHeaderValue("attachment");
            contentDisposition.SetHttpFileName(result.FileDownloadName);
            httpContext.Response.Headers.ContentDisposition = contentDisposition.ToString();
        }
    }

    private static void SetLastModifiedAndEtagHeaders(HttpResponse response, DateTimeOffset? lastModified, EntityTagHeaderValue? etag)
    {
        var httpResponseHeaders = response.GetTypedHeaders();
        if (lastModified.HasValue)
        {
            httpResponseHeaders.LastModified = lastModified;
        }

        if (etag != null)
        {
            httpResponseHeaders.ETag = etag;
        }
    }

    private static void SetAcceptRangeHeader(HttpResponse response)
    {
        response.Headers.AcceptRanges = AcceptedRangeHeaderValue;
    }

    private static PreconditionState GetMaxPreconditionState(params PreconditionState[] states)
    {
        var max = PreconditionState.Unspecified;
        for (var i = 0; i < states.Length; i++)
        {
            if (states[i] > max)
            {
                max = states[i];
            }
        }

        return max;
    }

    private static DateTimeOffset RoundDownToWholeSeconds(DateTimeOffset dateTimeOffset)
    {
        var ticksToRemove = dateTimeOffset.Ticks % TimeSpan.TicksPerSecond;
        return dateTimeOffset.Subtract(TimeSpan.FromTicks(ticksToRemove));
    }

    internal enum PreconditionState
    {
        Unspecified,
        NotModified,
        ShouldProcess,
        PreconditionFailed
    }
}