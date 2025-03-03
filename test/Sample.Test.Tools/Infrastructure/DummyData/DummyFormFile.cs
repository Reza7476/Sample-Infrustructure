using Microsoft.AspNetCore.Http;
using System.Reflection;
using Xunit.Sdk;

namespace LegalCrm.TestTools.Infrastructure.DummyData;

public class DummyFormFile : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        // داده‌های شبیه‌سازی شده برای IFormFile
        var fileId = "sampleId";
        var fileExtension = "txt";
        var fileName = "dummyFile.txt";
        var content = "This is a dummy file content for testing purposes.";

        // شبیه‌سازی فایل با استفاده از MemoryStream
        var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream);
        writer.Write(content);
        writer.Flush();
        memoryStream.Position = 0;

        var formFile = new FormFile(memoryStream, 0, memoryStream.Length, "file", fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "text/plain"
        };

        return new[] { new object[] { formFile } };
    }
}
