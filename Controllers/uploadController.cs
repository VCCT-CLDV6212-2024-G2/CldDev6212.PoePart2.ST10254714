using Microsoft.AspNetCore.Mvc;
using CldDev6212.Poe.Models;
using Microsoft.Extensions.Configuration;
using CldDev6212.Poe.AzServices;

namespace CldDev6212.Poe.Controllers

{

    public class uploadController : Controller
    {
        private readonly imageService _imageService;

        public uploadController(imageService imageService)
        {
            _imageService = imageService;
        }
        public async Task<IActionResult> uploadProductImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("upload a valid file type");
            using (var stream = file.OpenReadStream())
            {
                string fileName = $"{User.Identity?.Name ?? "unknown"}--{DateTime.UtcNow:yyyyMMddHHmmss}.jpg";
                await _imageService.uploadImageAsync("product-image", fileName, stream);
            }

            return Ok("Image succesfully uploaded");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

