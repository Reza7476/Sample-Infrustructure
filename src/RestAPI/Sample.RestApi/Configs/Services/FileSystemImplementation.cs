using Sample.Commons.Interfaces;

namespace Sample.RestApi.Configs.Services;

public class FileSystemImplementation : IFileSystem
{
    public void CreateDirectory(string path)
    {
        Directory.CreateDirectory(path);
    }

    public void Delete(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public bool Exists(string path)
    {
        return File.Exists(path) || Directory.Exists(path);
    }

    public string[] GetFiles(string directoryPath)
    {
        return Directory.GetFiles(directoryPath);
    }

    public void WriteAllBytes(string path, byte[] bytes)
    {
        File.WriteAllBytes(path, bytes);
    }

    public async Task WriteAllBytesAsync(string path, byte[] bytes)
    {
        await File.WriteAllBytesAsync(path, bytes);
    }
}
