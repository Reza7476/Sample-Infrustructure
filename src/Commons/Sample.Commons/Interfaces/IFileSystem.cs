namespace Sample.Commons.Interfaces;

public interface IFileSystem : IScope
{
    void CreateDirectory(string path); // برای ایجاد دایرکتوری
    void WriteAllBytes(string path, byte[] bytes); // برای نوشتن فایل
    bool Exists(string path); // برای چک کردن وجود فایل یا دایرکتوری
    Task WriteAllBytesAsync(string path, byte[] bytes); // برای نوشتن فایل به صورت async
    string[] GetFiles(string directoryPath);
}
