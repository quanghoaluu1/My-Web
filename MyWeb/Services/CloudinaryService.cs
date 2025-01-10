using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
namespace WebApplication1.Services;

public class CloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService()
    {
        DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
        _cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
        _cloudinary.Api.Secure = true;
    }

    public  string Upload(IFormFile file)
    {
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            Transformation = new Transformation().Width(500).Height(500).Crop("limit")
        };
        var uploadResult =  _cloudinary.Upload(uploadParams);
        return uploadResult.SecureUrl.AbsoluteUri;
    }

}