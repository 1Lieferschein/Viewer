using _1Lieferschein.Controllers.Utils;
using _1Lieferschein.Models;
using _1Lieferschein.Models.DeliveryNotes;
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

        public IActionResult Results() 
        {
            return View();
        }

        [HttpPost("Results")]
        public async Task<ActionResult> UploadFile(IFormFile[] fileUpload)
        {
            // IFormFile upload = fileUpload[0];
            DeliveryNoteUbl3 returnModel = new DeliveryNoteUbl3();

            for (int i = 0; i < fileUpload.Length; i++)
            {
                IFormFile uploadFile = fileUpload[i];

                String contentType = uploadFile.ContentType;
                bool IsValidDoc = false;
                if (!string.IsNullOrEmpty(contentType) && contentType.Equals(MediaTypeNames.Text.Xml))
                {
                    IsValidDoc = true;
                }

                if (IsValidDoc == false)
                {
                    ViewBag.Message = "Fehler: Ungültiges Dokument";
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                    // return (ActionResult)Error();
                }

                using (var ms = new MemoryStream())
                {
                    try
                    {
                        DespatchAdvice despatchAdvice = Common.Deserialize<DespatchAdvice>(await Common.ReadAsStringAsync(uploadFile));
                        ViewBag.Message = "XML erfolgreich hochgeladen.";

                        switch (i)
                        {
                            case 0:
                                returnModel.DespatchAdvice1 = despatchAdvice;
                                break;
                            case 1:
                                returnModel.DespatchAdvice2 = despatchAdvice;
                                break;
                        }
                    }
                    catch (XmlException xmlException)
                    {
                        ViewBag.Message = "Fehler: " + xmlException.Message + ", inner-exception: " + xmlException?.InnerException?.Message + ", strack-trace: " + xmlException.StackTrace;
                        return View();
                    }
                }

            }
            // return View(returnModel);

            return View("Results", returnModel);
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
