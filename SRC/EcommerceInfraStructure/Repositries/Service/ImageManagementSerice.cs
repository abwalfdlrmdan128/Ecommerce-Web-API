using Ecommerce.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceInfraStructure.Repositries.Service
{
    public class ImageManagementSerice : IImageManagementService
    {
        private readonly IFileProvider _FileProvider;
        public ImageManagementSerice(IFileProvider fileProvider)
        {
            _FileProvider = fileProvider;
        }

        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            var SaveImageSrc = new List<string>();
            var ImageDirctory = Path.Combine("wwwroot", "Images", src);
            if (!Directory.Exists(ImageDirctory)) { 
             Directory.CreateDirectory(ImageDirctory);
            }
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    // get image name
                    var ImageName = file.FileName;
                    var ImageSource = $"Images/{src}/{ImageName}";
                    var root=Path.Combine(ImageDirctory, ImageName);
                    using(FileStream stream=new FileStream(root, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    SaveImageSrc.Add(ImageSource);
                } 
            }
            return SaveImageSrc;
        }

        public void DeleteImageAsync(string src)
        {
            var Info=_FileProvider.GetFileInfo(src);
            var root = Info.PhysicalPath;
            File.Delete(root);
        }
    }
}
