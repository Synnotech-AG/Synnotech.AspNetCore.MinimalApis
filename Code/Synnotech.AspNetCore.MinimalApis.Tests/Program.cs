using Microsoft.AspNetCore.Builder;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public static class WebAppFactory
{
    public static WebApplication Create()
    {
        var builder = WebApplication.CreateBuilder();

        var app = builder.Build();

        app.AddStatusCodeResponses();
        app.AddObjectResponses();
        app.AddRedirectAndForbiddenResponses();
        app.AddFileResponses();

        return app;
    }
}
