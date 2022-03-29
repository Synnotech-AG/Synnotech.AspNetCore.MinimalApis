using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public sealed class AuthenticationServiceStub : IAuthenticationService
{
    public Task<AuthenticateResult> AuthenticateAsync(HttpContext context, string? scheme)
    {
        throw new NotSupportedException();
    }

    public Task ChallengeAsync(HttpContext context, string? scheme, AuthenticationProperties? properties)
    {
        throw new NotSupportedException();
    }

    public Task ForbidAsync(HttpContext context, string? scheme, AuthenticationProperties? properties)
    {
        context.Response.StatusCode = 403;
        return Task.CompletedTask;
    }

    public Task SignInAsync(HttpContext context, string? scheme, ClaimsPrincipal principal, AuthenticationProperties? properties)
    {
        throw new NotSupportedException();
    }

    public Task SignOutAsync(HttpContext context, string? scheme, AuthenticationProperties? properties)
    {
        throw new NotSupportedException();
    }
}