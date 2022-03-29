using System.IO;

namespace Synnotech.AspNetCore.MinimalApis.Tests;

public static class FileHelper
{
    public static long GetFileLength(string filePath)
    {
        return new FileInfo(filePath).Length;
    }

    public static long GetStreamLength(Stream stream)
    {
        var originalPosition = 0L;
        var totalBytesRead = 0L;

        if (stream.CanSeek)
        {
            originalPosition = stream.Position;
            stream.Position = 0;
        }

        try
        {
            var readBuffer = new byte[4096];
            int bytesRead;

            while ((bytesRead = stream.Read(readBuffer, 0, 4096)) > 0)
            {
                totalBytesRead += bytesRead;
            }
        }
        finally
        {
            if (stream.CanSeek)
            {
                stream.Position = originalPosition;
            }
        }

        return totalBytesRead;
    }
}