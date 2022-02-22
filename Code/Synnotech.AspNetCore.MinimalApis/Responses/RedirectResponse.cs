using System;
using System.Threading.Tasks;
using Light.GuardClauses;
using Microsoft.AspNetCore.Http;
using Synnotech.AspNetCore.MinimalApis.Responses.Internals;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 307/308 Redirect response.
/// </summary>
public sealed class RedirectResponse : IResult
{
    /// <summary>
    /// Initializes a new instance of <see cref="RedirectResponse" />.
    /// </summary>
    /// <param name="url">The URL to redirect to.</param>
    /// <param name="permanent">Specifies whether the redirect should be permanent (301) or temporary (302).</param>
    /// <param name="preserveMethod">If set to true, make the temporary redirect (307) or permanent redirect (308) preserve the initial request method.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="url"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="url"/> is empty.</exception>
    public RedirectResponse(string url, bool permanent, bool preserveMethod)
    {
        url.MustNotBeNullOrEmpty();

        Url = url;
        Permanent = permanent;
        PreserveMethod = preserveMethod;
    }

    /// <summary>
    /// Gets or sets the URL to redirect to.
    /// </summary>
    public string Url { get; }

    /// <summary>
    /// Gets the value that specifies that the redirect should be permanent if true or temporary if false.
    /// </summary>
    public bool Permanent { get; }

    /// <summary>
    /// Gets an indication that the redirect preserves the initial request method.
    /// </summary>
    public bool PreserveMethod { get; }


    /// <inheritdoc />
    public Task ExecuteAsync(HttpContext httpContext)
    {
        var destinationUrl = SharedUrlHelper.IsLocalUrl(Url) ? SharedUrlHelper.Content(httpContext, Url) : Url;

        if (PreserveMethod)
        {
            httpContext.Response.StatusCode = Permanent ? StatusCodes.Status308PermanentRedirect : StatusCodes.Status307TemporaryRedirect;
            httpContext.Response.Headers.Location = destinationUrl;
        }
        else
        {
            httpContext.Response.Redirect(destinationUrl!, Permanent);
        }

        return Task.CompletedTask;
    }
}