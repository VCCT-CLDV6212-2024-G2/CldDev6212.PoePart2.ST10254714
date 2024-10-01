using CldDev6212.Poe.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using CldDev6212.Poe.AzServices;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using Azure.Storage;
///////////////////////////////////////////////////////////////
///                 references:
///                 www.Claude.ai
///                 https://www.w3schools.com/Asp/webpages_intro.asp
///                 https://learn.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=net80&pivots=development-environment-vs
///                 www.chatgpt.com



namespace CldDev6212.Poe.Controllers
{
    
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

            private readonly blobStorage _blobStorage;
            private readonly tableService _tableService;
            private readonly queueService _queueService;
            private readonly fileService _fileService;




            public HomeController(blobStorage blobStorage, tableService tableService, queueService queueService, fileService fileService)
            {
                _blobStorage = blobStorage;
                _tableService = tableService;
                _queueService = queueService;
                _fileService = fileService;
            }
            
            public IActionResult Index()
        {
            return View();
        }
            
            [HttpPost("addCustomer")]

            public async Task<IActionResult> addCustomer(CustomerProfile profile)
            {
                if (ModelState.IsValid)
                {
                    await _tableService.AddEntityAsync(profile);
                }
                return RedirectToAction("Index");
                
            }



            [HttpPost("uploadImage")]
            public async Task<IActionResult> uploadImage(IFormFile file)
            {
                if (file != null)
                {
                    using var stream = file.OpenReadStream();
                    await _blobStorage.UploadBlobAsync("product-images", file.FileName, stream);
                    //var blobClient = await _blobStorage.UploadBlobAsync("images", file.FileName, stream);
                    //await _queueService.AddMessageToQueue("imageprocessing", $"Upload image {file.FileName}");
                    //TempData["message"] = $"Image uploaded succesfully";
                }
                return RedirectToAction("Index");

            }

            /*[HttpPost]
            public async Task<IActionResult> addCustomerProfile(CustomerProfile profile)
            {
                if (ModelState.IsValid)
                {
                    await _tableService.AddEntityAsync(profile);
                }
                return RedirectToAction("Index");
            }*/

            [HttpPost("processOrder")]
            public async Task<IActionResult> processOrder(string orderId)
            {
                await _queueService.SendMessageAsync("order-processing", $"Processing order {orderId}");
                TempData["message"] = "Order processed";
                return RedirectToAction("Index");
            }

            [HttpPost("uploadContract")]
            public async Task<IActionResult> uploadContract(IFormFile file)
            {
                if (file != null)
                {
                    using var stream = file.OpenReadStream();
                    await _fileService.UploadFileAsync("contracts", file.FileName, stream);
                    TempData["message"] = "Contract submitted succesfully";

                }
                return RedirectToAction("Index");
            }

            [HttpPost("uploadLog")]
            public async Task<IActionResult> uploadLog(string logContent)
            {
                using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(logContent));
                await _fileService.UploadFileAsync("logs", $"log_{DateTime.UtcNow:yyyyMMddHHmmss}.txt", stream);
                TempData["message"] = "Log added succesfully";
                return RedirectToAction("Index");
            }

        /*[HttpPost("addProduct")]
        public async Task<IActionResult> addProduct(Product product)
        {
            if(product == null)
            {
                return BadRequest("please enter product data");
            }

            if (ModelState.IsValid)
            {
                await _tableService.AddEntityAsync(product);
            }
            return RedirectToAction("Index");

        }*/
        

    }
        
}
