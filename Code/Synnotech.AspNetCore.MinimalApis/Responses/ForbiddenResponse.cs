using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Responses;

/// <summary>
/// Represents the HTTP 403 Forbidden response.
/// </summary>
public sealed class ForbiddenResponse : IResult
{
    /// <summary>
    /// Initializes a new instance of <see cref="ForbiddenResponse" />.
    /// </summary>
    public ForbiddenResponse()
        : this(Array.Empty<string>()) { }

    /// <summary>
    /// Initializes a new instance of <see cref="ForbiddenResponse" />
    /// with the specified <paramref name="authenticationScheme" />.
    /// </summary>
    /// <param name="authenticationScheme">The authentication scheme to challenge.</param>
    public ForbiddenResponse(string authenticationScheme)
        : this(new[] { authenticationScheme }) { }

    /// <summary>
    /// Initializes a new instance of <see cref="ForbiddenResponse" />
    /// with the specified <paramref name="authenticationSchemes" />.
    /// </summary>
    /// <param name="authenticationSchemes">The authentication schemes to challenge.</param>
    public ForbiddenResponse(IList<string> authenticationSchemes)
        : this(authenticationSchemes, properties: null) { }

    /// <summary>
    /// Initializes a new instance of <see cref="ForbiddenResponse" />
    /// with the specified <paramref name="properties" />.
    /// </summary>
    /// <param name="properties"><see cref="AuthenticationProperties" /> used to perform the authentication challenge.</param>
    public ForbiddenResponse(AuthenticationProperties? properties)
        : this(Array.Empty<string>(), properties) { }

    /// <summary>
    /// Initializes a new instance of <see cref="ForbiddenResponse" />
    /// with the specified <paramref name="authenticationScheme" /> and <paramref name="properties" />.
    /// </summary>
    /// <param name="authenticationScheme">The authentication scheme to challenge.</param>
    /// <param name="properties"><see cref="AuthenticationProperties" /> used to perform the authentication challenge.</param>
    public ForbiddenResponse(string authenticationScheme, AuthenticationProperties? properties)
        : this(new[] { authenticationScheme }, properties) { }

    /// <summary>
    /// Initializes a new instance of <see cref="ForbiddenResponse" />
    /// with the specified <paramref name="authenticationSchemes" /> and <paramref name="properties" />.
    /// </summary>
    /// <param name="authenticationSchemes">The authentication scheme to challenge.</param>
    /// <param name="properties"><see cref="AuthenticationProperties" /> used to perform the authentication challenge.</param>
    public ForbiddenResponse(IList<string> authenticationSchemes, AuthenticationProperties? properties)
    {
        AuthenticationSchemes = authenticationSchemes;
        Properties = properties;
    }

    /// <summary>
    /// Gets the authentication schemes that are challenged.
    /// </summary>
    public IList<string> AuthenticationSchemes { get; init; }

    /// <summary>
    /// Gets or sets the <see cref="AuthenticationProperties" /> used to perform the authentication challenge.
    /// </summary>
    public AuthenticationProperties? Properties { get; init; }


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