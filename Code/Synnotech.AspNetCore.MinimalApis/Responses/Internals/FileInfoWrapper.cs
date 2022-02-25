using System;
using System.IO;

namespace Synnotech.AspNetCore.MinimalApis.Responses.Internals;

/// <summary>
/// Provides infos for a file in the provided path.
/// </summary>
public readonly struct FileInfoWrapper
{
    /// <summary>
    /// Creates a new <see cref="FileInfoWrapper"/> instance.
    /// </summary>
    /// <param name="path">The path to the file on the disk.</param>
    public FileInfoWrapper(string path)
    {
        var fileInfo = new FileInfo(path);

        if (fileInfo.Exists && !string.IsNullOrEmpty(fileInfo.LinkTarget))
        {
            fileInfo = (FileInfo?)fileInfo.ResolveLinkTarget(returnFinalTarget: true)
                    ?? fileInfo;
        }

        Exists = fileInfo.Exists;
        Length = fileInfo.Length;
        LastWriteTimeUtc = fileInfo.LastWriteTimeUtc;
    }

    /// <summary>
    /// Gets information if a file exists in the provided path.
    /// </summary>
    public bool Exists { get; init; }

    /// <summary>
    /// Gets the file-length in byte.
    /// </summary>
    public long Length { get; init; }

    /// <summary>
    /// Gets the last time and date where the file was modified or written.
    /// </summary>
    public DateTimeOffset LastWriteTimeUtc { get; init; }
}