using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSEI_aspnet_projekt.Services
{
	public interface IFileService
	{
		public string SaveImage(IFormFile imageFile);
		public string GetImagePath(string imageName);
	}
}
