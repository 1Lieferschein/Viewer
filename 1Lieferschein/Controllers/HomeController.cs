using _1Lieferschein.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace _1Lieferschein.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<FormFile> UploadFile(IFormFile fileUpload)
        {
            String contentType = fileUpload.ContentType;
            if (!string.IsNullOrEmpty(contentType))
            {
                if (contentType.Equals(MediaTypeNames.Text.Xml))
                {
                    using (var ms = new MemoryStream())
                    {
                        await fileUpload.OpenReadStream().CopyToAsync(ms);

                        XmlDocument doc = new XmlDocument();
                        doc.Load(ms);
                    }
                    //todo Uploadmodel constructor
                    return null; 
                }
                else
                {
                    return null; 
                }
            }
            else
            {
                return null; 
            }
        }

        private byte[] ConvertToByte(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.OpenReadStream().CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
