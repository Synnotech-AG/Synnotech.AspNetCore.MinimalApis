using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents either an HTTP 401 Unauthorized or an HTTP 403 Forbidden response.
/// The response will forward the result to the <see cref="IAuthenticationService" />
/// registered with your DI container. The authentication service will then decide
/// based on your configuration if a 401 with a challenge scheme or a 403 forbidden
/// will be returned.
/// </summary>
public sealed class ForbidResponse : IResult
{
    /// <summary>
    /// Initializes a new instance of <see cref="ForbidResponse" />.
    /// </summary>
    public ForbidResponse()
        : this(Array.Empty<string>()) { }

    /// <summary>
    /// Initializes a new instance of <see cref="ForbidResponse" />
    /// with the specified <paramref name="authenticationScheme" />.
    /// </summary>
    /// <param name="authenticationScheme">The authentication scheme to challenge.</param>
    public ForbidResponse(string authenticationScheme)
        : this(new[] { authenticationScheme }) { }

    /// <summary>
    /// Initializes a new instance of <see cref="ForbidResponse" />
    /// with the specified <paramref name="authenticationSchemes" />.
    /// </summary>
    /// <param name="authenticationSchemes">The authentication schemes to challenge.</param>
    public ForbidResponse(IList<string> authenticationSchemes)
        : this(authenticationSchemes, null) { }

    /// <summary>
    /// Initializes a new instance of <see cref="ForbidResponse" />
    /// with the specified <paramref name="properties" />.
    /// </summary>
    /// <param name="properties"><see cref="AuthenticationProperties" /> used to perform the authentication challenge.</param>
    public ForbidResponse(AuthenticationProperties? properties)
        : this(Array.Empty<string>(), properties) { }

    /// <summary>
    /// Initializes a new instance of <see cref="ForbidResponse" />
    /// with the specified <paramref name="authenticationScheme" /> and <paramref name="properties" />.
    /// </summary>
    /// <param name="authenticationScheme">The authentication scheme to challenge.</param>
    /// <param name="properties"><see cref="AuthenticationProperties" /> used to perform the authentication challenge.</param>
    public ForbidResponse(string authenticationScheme, AuthenticationProperties? properties)
        : this(new[] { authenticationScheme }, properties) { }

    /// <summary>
    /// Initializes a new instance of <see cref="ForbidResponse" />
    /// with the specified <paramref name="authenticationSchemes" /> and <paramref name="properties" />.
    /// </summary>
    /// <param name="authenticationSchemes">The authentication scheme to challenge.</param>
    /// <param name="properties"><see cref="AuthenticationProperties" /> used to perform the authentication challenge.</param>
    public ForbidResponse(IList<string> authenticationSchemes, AuthenticationProperties? properties)
    {
        AuthenticationSchemes = authenticationSchemes;
        Properties = properties;
    }

    /// <summary>
    /// Gets the authentication schemes that are challenged.
    /// </summary>
    public IList<string> AuthenticationSchemes { get; }

    /// <summary>
    /// Gets or sets the <see cref="AuthenticationProperties" /> used to perform the authentication challenge.
    /// </summary>
    public AuthenticationProperties? Properties { get; }


    /// <inheritdoc />
    public async Task ExecuteAsync(HttpContext httpContext)
    {
        if (AuthenticationSchemes.Count > 0)
        {
            for (var i = 0; i < AuthenticationSchemes.Count; i++)
            {
                await httpContext.ForbidAsync(AuthenticationSchemes[i], Properties);
            }
        }
        else
        {
            await httpContext.ForbidAsync(Properties);
        }
    }
}