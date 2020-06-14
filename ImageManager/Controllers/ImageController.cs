using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using ImageManager.Services.Repositories.ImageRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("writeImage")]
        public async Task<int> WriteImage(IFormFile file)
        {
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "public/images"); ;

            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            return StatusCodes.Status200OK;
        }
    }
}