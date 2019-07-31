namespace Olympia.Services.Utilities
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;

    using Microsoft.AspNetCore.Http;
    using System.IO;

    public static class MyCloudinary
    {
        public static string UploadImage(IFormFile fileform, string articleTitle)
        {
            var cloudinary = SetCloudinary();

            if (fileform == null)
            {
                return null;
            }

            byte[] articleImg;

            using (var memoryStream = new MemoryStream())
            {
                fileform.CopyTo(memoryStream);
                articleImg = memoryStream.ToArray();
            }

            ImageUploadResult uploadResult;

            using (var ms = new MemoryStream(articleImg))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(articleTitle, ms),
                    Transformation = new Transformation(),
                };

                uploadResult = cloudinary.Upload(uploadParams);
            }

            if (uploadResult.Error != null)
            {
                return "shit...";
            }

            return uploadResult.SecureUri.AbsoluteUri;
        }

        private static Cloudinary SetCloudinary()
        {
            CloudinaryDotNet.Account account = new CloudinaryDotNet.Account
            {
                Cloud = Constants.CloudinaryCloudName,
                ApiKey = Constants.CloudinaryApiKey,
                ApiSecret = Constants.CloudinaryApiSecret,
            };

            Cloudinary cloudinary = new Cloudinary(account);
            return cloudinary;
        }
    }
}
