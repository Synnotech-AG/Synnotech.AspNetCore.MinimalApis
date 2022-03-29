using System.IO;

namespace Synnotech.AspNetCore.MinimalApis.Tests.DefaultValues;

public sealed class TestFile
{
    public static TestFile ExcelDefault { get; } = new ()
    {
        FileName = "TestFile.xlsx",
        FilePath = Path.Combine("DefaultValues", "TestFile.xlsx")
    };

    public string FileName { get; init; } = string.Empty;

    public string FilePath { get; init; } = string.Empty;
}