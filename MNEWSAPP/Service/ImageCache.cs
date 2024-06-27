using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNEWSAPP.Service;

public static class ImageCache
{
    private static readonly HttpClient _httpClient = new HttpClient();

    public static async Task<string> GetImageFromCacheAsync(string? url)
    {
        var cacheDir = FileSystem.CacheDirectory;
        var fileName = Path.Combine(cacheDir, Path.GetFileName(url));

        if (File.Exists(fileName))
        {
            return fileName;
        }
        else
        {
            var imageBytes = await _httpClient.GetByteArrayAsync(url);
            File.WriteAllBytes(fileName, imageBytes);
            return fileName;
        }
    }
}
