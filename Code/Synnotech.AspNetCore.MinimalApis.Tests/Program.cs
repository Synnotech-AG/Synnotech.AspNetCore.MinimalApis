using Microsoft.AspNetCore.Builder;
using Synnotech.AspNetCore.MinimalApis.Tests;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.AddStatusCodeResponses();
app.AddObjectResponses();
app.AddRedirectAndForbiddenResponses();

app.Run();

// for integration testing
public partial class Program { }