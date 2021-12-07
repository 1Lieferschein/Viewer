<<<<<<< HEAD
﻿using _1Lieferschein.Controllers.Utils;
using _1Lieferschein.Models;
using _1Lieferschein.Models.DeliveryNotes;
using Microsoft.AspNetCore.Http;
=======
﻿using _1Lieferschein.Models;
>>>>>>> 332d876eae3c29a0ecfd6d016fd27d0627302b31
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
<<<<<<< HEAD
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
=======
>>>>>>> 332d876eae3c29a0ecfd6d016fd27d0627302b31

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

<<<<<<< HEAD
        [HttpPost]
        public async Task<ActionResult> UploadFile(IFormFile fileUpload)
        {
            String contentType = fileUpload.ContentType;
            if (!string.IsNullOrEmpty(contentType))
            {
                if (contentType.Equals(MediaTypeNames.Text.Xml))
                {
                    using (var ms = new MemoryStream())
                    {
                        try
                        {
                            DespatchAdvice despatchAdvice = Common.Deserialize<DespatchAdvice>(await Common.ReadAsStringAsync(fileUpload));
                            ViewBag.Message = "XML erfolgreich hochgeladen.";
                            return View(despatchAdvice);
                        }
                        catch (XmlException xmlException)
                        {
                            ViewBag.Message = "Fehler: " + xmlException.Message + ", inner-exception: " + xmlException?.InnerException?.Message + ", strack-trace: " + xmlException.StackTrace;
                            return View();
                        }
                    }
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

=======
>>>>>>> 332d876eae3c29a0ecfd6d016fd27d0627302b31
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
