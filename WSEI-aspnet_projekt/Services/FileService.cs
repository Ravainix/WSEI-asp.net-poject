using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WSEI_aspnet_projekt.Services
{
	public class FileService : IFileService
	{
		private readonly IWebHostEnvironment _hostEnvironment;

		public FileService(
			IWebHostEnvironment hostEnvironment)
		{
			_hostEnvironment = hostEnvironment;
		}

		public string SaveImage(IFormFile imageFile)
		{
			string imageName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyMMdd") + Path.GetExtension(imageFile.FileName);
			var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
			using (var fileStream = new FileStream(imagePath, FileMode.Create))
			{
				imageFile.CopyTo(fileStream);
			}
			return imageName;
		}

		public string GetImagePath(string imageName)
		{
			return Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
		}
	}
}
