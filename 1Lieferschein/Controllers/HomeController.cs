using _1Lieferschein.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        public async Task Create(IFormFile fileUpload)
        {
            XmlReaderSettings settings = new XmlReaderSettings(); settings.Async = true;

            //byte[] buffer = new byte[fileUpload.Length];
            //var result = ConvertToByte(fileUpload);

            using (var ms = new MemoryStream())
            {
                fileUpload.OpenReadStream().CopyTo(ms);

                if (ms.Position > 0)
                {
                    ms.Position = 0;
                }

                // sr.ReadToEnd();
                XmlDocument doc = new XmlDocument();
                doc.Load(ms);

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
