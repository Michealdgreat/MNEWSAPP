using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MNEWSAPP.Service
{
    public static class ImageCache
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<string> GetImageFromCacheAsync(string? url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));

            var cacheDir = FileSystem.CacheDirectory;
            var fileName = GetHashedFileName(url);
            var filePath = Path.Combine(cacheDir, fileName);

            if (File.Exists(filePath))
            {
                return filePath;
            }
            else
            {
                var imageBytes = await _httpClient.GetByteArrayAsync(url);
                await File.WriteAllBytesAsync(filePath, imageBytes);
                return filePath;
            }
        }

        private static string GetHashedFileName(string url)
        {
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(url));
                var fileName = BitConverter.ToString(hash).Replace("-", "").ToLower();
                return fileName;
            }
        }
    }
}
