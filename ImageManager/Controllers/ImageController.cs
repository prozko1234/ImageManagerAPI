using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using ImageManager.Services.Repositories.ImageRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ImageManager.EntityFramework.Models;

namespace ImageManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly IImageRepository _imageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageController(IImageRepository imageRepository, IHttpContextAccessor httpContextAccessor)
        {
            _imageRepository = imageRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("writeImage")]
        public async Task<int> WriteImage(IFormFile file, int id)
        {
            string link;
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "public/images");
            try
            {
                link = _httpContextAccessor.HttpContext.Request.Host.Value;
            }catch(Exception e)
            {
                return StatusCodes.Status404NotFound;
            }

            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            Image image = new Image
            {
                Name = file.FileName,
                CreatedDate = DateTime.Now,
                ParentId = id,
                FilePath = Path.Combine(uploads, file.FileName),
                FileLink = "https://" + link + "/images/" + file.FileName
        };

            _imageRepository.AddImage(image);

            return StatusCodes.Status200OK;
        }

        [HttpGet("getAllImages")]
        public IActionResult GetAallImages()
        {
            var images = _imageRepository.GetAllImages();
            return Json(images);
        }
    }
}