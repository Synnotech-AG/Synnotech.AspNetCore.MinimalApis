using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public class WebHostEnvironmentStub : IWebHostEnvironment
{
    public WebHostEnvironmentStub()
    {
        ApplicationName = "BaseWebAppTest";
        ContentRootFileProvider = new NullFileProvider();
        ContentRootPath = (Environment.CurrentDirectory).Split(@"bin\Debug")[0];
        EnvironmentName = "Synnotech.AspNetCore.MinimalApis.Test";
        WebRootPath = ContentRootPath;
        WebRootFileProvider = new NullFileProvider();
    }

    public string ApplicationName { get; set; }
    public IFileProvider ContentRootFileProvider { get; set; }
    public string ContentRootPath { get; set; }
    public string EnvironmentName { get; set; }
    public string WebRootPath { get; set; }
    public IFileProvider WebRootFileProvider { get; set; }
}