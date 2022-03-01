using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// A <see cref="VirtualFileResponse"/> that on execution writes the file specified
/// using a virtual path to the response using mechanisms provided by the host.
/// </summary>
public class VirtualFileResponse : FileResponse
{
    private IFileInfo? _fileInfo;
    private string _fileName;

    /// <summary>
    /// Creates a new <see cref="VirtualFileResponse"/> instance with the provided <paramref name="fileName"/>
    /// and the provided <paramref name="contentType"/>.
    /// </summary>
    /// <param name="fileName">The path to the file. The path must be relative/virtual.</param>
    /// <param name="contentType">The Content-Type header of the response.</param>
    /// <exception cref="ArgumentNullException">Thrown when the fileName is null.</exception>
    public VirtualFileResponse(string fileName, string? contentType) : base(contentType)
    {
        FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
    }

    /// <summary>
    /// Gets or sets the path to the file that will be sent back as the response.
    /// </summary>
    /// <exception cref="ArgumentNullException">Gets thrown when the value is null.</exception>
    public string FileName
    {
        get => _fileName;
        [MemberNotNull(nameof(_fileName))]
        set => _fileName = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <inheritdoc />
    protected override Task ExecuteCoreAsync(HttpContext httpContext, RangeItemHeaderValue? range, long rangeLength)
    {
        var response = httpContext.Response;
        var offset = 0L;
        var count = (long?) null;

        if (range != null)
        {
            offset = range.From ?? 0L;
            count = rangeLength;
        }

        return response.SendFileAsync(_fileInfo!,
                                      offset,
                                      count);
    }

    /// <inheritdoc />
    public override Task ExecuteAsync(HttpContext httpContext)
    {
        var hostingEnvironment = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();

        var fileInfo = GetFileInformation(hostingEnvironment.WebRootFileProvider);
        if (!fileInfo.Exists)
        {
            throw new FileNotFoundException($"Could not find file: {FileName}.", FileName);
        }

        _fileInfo = fileInfo;
        LastModified ??= fileInfo.LastModified;
        FileLength = fileInfo.Length;

        return base.ExecuteAsync(httpContext);
    }

    private IFileInfo GetFileInformation(IFileProvider fileProvider)
    {
        var normalizedPath = FileName;
        if (normalizedPath.StartsWith("~", StringComparison.Ordinal))
        {
            normalizedPath = normalizedPath.Substring(1);
        }

        var fileInfo = fileProvider.GetFileInfo(normalizedPath);
        return fileInfo;
    }
}