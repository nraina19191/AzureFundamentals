using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace AzureFunction;

public class ResizeImageOnBlobUpload
{
    private readonly ILogger<ResizeImageOnBlobUpload> _logger;

    public ResizeImageOnBlobUpload(ILogger<ResizeImageOnBlobUpload> logger)
    {
        _logger = logger;
    }

    [Function(nameof(ResizeImageOnBlobUpload))]
    [BlobOutput("functionsalesrep-output/{name}")]
    public async Task<byte[]> Run([BlobTrigger("functionsalesrep/{name}", Connection = "")] byte[] blobByte, string name)
    {
        using var memoryStream = new MemoryStream(blobByte);
        using var image = SixLabors.ImageSharp.Image.Load(memoryStream);
        image.Mutate(x => x.Resize(100, 100));
        using var outputStream = new MemoryStream();
        image.SaveAsJpeg(outputStream);
        outputStream.Position = 0;
        _logger.LogInformation($"C# Blob trigger resize image");

        return outputStream.ToArray();
    }
}