using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StoreApp.NetFramework.Services
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary cloudinary;

        private readonly string pictureSize;
        public ImageService()
        {
            pictureSize = ConfigurationManager.AppSettings["PictureSize"];
            cloudinary = new Cloudinary(new Account(ConfigurationManager.AppSettings["CloudName"], 
                ConfigurationManager.AppSettings["ApiKey"], ConfigurationManager.AppSettings["ApiSecret"]));

        }
        public string UploadImage(IFormFile image)
        {
            var pixSize = Convert.ToInt64(pictureSize);

            if (image == null || image.Length > pixSize)
                throw new FormatException("File size should not exceed 2mb");

            //object to return
            var uploadResult = new ImageUploadResult();

            //fetch image as stream of data
            using (var imageStream = image.OpenReadStream())
            {
                string fileName = Guid.NewGuid().ToString() + "_" + image.Name;
                //upload to cloudinary
                uploadResult = Task.Run(async () => await cloudinary.UploadAsync(new ImageUploadParams()
                {
                    File = new FileDescription(fileName, imageStream),
                    Transformation = new Transformation().Crop("thumb").Gravity("face").Width(1000)
                                                        .Height(1000).Radius(40)
                })).Result;
            }

            return uploadResult.Url.ToString();
        }
    }
}