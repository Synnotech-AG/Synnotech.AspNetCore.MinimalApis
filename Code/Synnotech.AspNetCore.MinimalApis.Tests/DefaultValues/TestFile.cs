using System;
using System.IO;

namespace Synnotech.AspNetCore.MinimalApis.Tests.DefaultValues;

public sealed class TestFile
{
    public static TestFile ExcelDefault { get; } = new ()
    {
        FileName = @"DefaultValues\TestFile.xlsx",
        FilePath = Path.Combine((Environment.CurrentDirectory).Split(@"bin\Debug")[0], @"DefaultValues\TestFile.xlsx")
    };

    public string? FileName { get; init; }

    public string? FilePath { get; init; }
}