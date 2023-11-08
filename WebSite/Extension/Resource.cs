namespace WebSite.Extension
{
    public class Resource
    {
        public static async Task<byte[]> ReadAllBytesAsync(string path)
        {
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string fullPath = Path.Combine(projectDirectory, path);

            return await System.IO.File.ReadAllBytesAsync(fullPath);
        }
    }
}
