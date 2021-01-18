using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WSEI_aspnet_projekt.Models;
using WSEI_aspnet_projekt.Services;

namespace WSEI_aspnet_projekt.Controllers
{
	[Route("api/")]
	[ApiController]
	public class FilesController : ControllerBase
	{
        private readonly List<string> allowedImagesExtensions = new List<string> {"image/jpg", "image/png"};
        private readonly int maxImageLength = 10485760;

        private readonly IFileService _filesService;

		public FilesController(IFileService filesService)
		{
			_filesService = filesService;
		}

        // POST: api/images/upload
        [Authorize]
        [HttpPost("images/upload")]
        public ActionResult<MyResponse> UploadImage(IFormFile imageFile)
        {
            MyResponse validateResult = ValidateImage(imageFile);
            if (!validateResult.Success) return validateResult;

            string imageName = _filesService.SaveImage(imageFile);
            string imageGetUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/api/images/get/{imageName}";
            return new MyResponse(true, imageGetUrl);
        }

        // GET: api/images/get{name}
        [HttpGet("images/get/{name}")]
        public ActionResult<string> GetImage(string name)
        {
            return PhysicalFile(_filesService.GetImagePath(name), "image/jpeg");
        }

        private MyResponse ValidateImage(IFormFile imageFile)
        {
            MyResponse result = new MyResponse(true);
            if (!allowedImagesExtensions.Contains(imageFile.ContentType))
            {
                result.Success = false;
                result.Message = "Invalid image type. Allowed types: jpg, png";
            } else if (imageFile.Length > maxImageLength)
            {
                result.Success = false;
                result.Message = "Image size too large. Maximum size is 10MB";
            }
            return result;
        }
    }
}
