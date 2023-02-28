using _1Lieferschein.Controllers.Utils;
using _1Lieferschein.Models;
using _1Lieferschein.Models.DeliveryNotes;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.ConstrainedExecution;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

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
        public async Task<ActionResult> UploadFile(IFormFile fileUpload)
        {
            var doc = new XmlDocument();

            //check how many xmls are uploaded
            for (int i = 0; i < fileUpload.Length; i++)
            {
                IFormFile uploadFile = fileUpload;

                //check how if the uploaded file is a xml
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
                }
                
                
                using (var ms = new MemoryStream())
                {
                    try
                    {                       

                        var serializer = new XmlSerializer(typeof(DespatchAdvice));
                        var despatchAdvice = serializer.Deserialize(fileUpload.OpenReadStream());




                        doc.LoadXml((string)despatchAdvice);

                        //doc.LoadXml("<?xml version=\"1.0\" encoding=\"UTF - 8\"?> \n" +
                        //    "<DespatchAdvice xmlns = \"urn:oasis:names:specification:ubl:schema:xsd:DespatchAdvice-2\" xmlns: cac = \"urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2\" xmlns: cbc = \"urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2\" xmlns: cec = \"urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2\" xmlns: csc = \"urn:oasis:names:specification:ubl:schema:xsd:CommonSignatureComponents-2\"> \n" +
                        //    "   <cac:OrderReference> \n" +
                        //    "       <cbc:ID> 1528 </cbc:ID> \n" +
                        //    "       <cbc:SalesOrderID> 4500274495 </cbc:SalesOrderID> \n" +
                        //    "       <cbc:CustomerReference> Demo_Baustelle_1 </cbc:CustomerReference> \n" +
                        //    "   </cac:OrderReference> \n" +
                        //    "</DespatchAdvice>"
                        //    );


                        //doc.LoadXml("<book genre='novel' ISBN='1-861001-57-5'>" +
                        //  "<title>Pride And Prejudice</title>" +
                        //  "</book>");

                        XmlNode root = doc.FirstChild;




                        //DespatchAdvice despatchAdvice = Common.Deserialize<DespatchAdvice>(await Common.ReadAsStringAsync(uploadFile));
                        //ViewBag.Message = "XML erfolgreich hochgeladen.";


                        //var despatch = new DespatchAdvice();
                        //XmlDocument j = despatch;


                        //doc = despatchAdvice;

                        //switch (i)
                        //{
                        //    case 0:
                        //        returnModel.DespatchAdvice1 = despatchAdvice;
                        //        break;
                        //    case 1:
                        //        returnModel.DespatchAdvice2 = despatchAdvice;
                        //        break;
                        //}



                    }
                    catch (XmlException xmlException)
                    {
                        ViewBag.Message = "Fehler: " + xmlException.Message + ", inner-exception: " + xmlException?.InnerException?.Message + ", strack-trace: " + xmlException.StackTrace;
                        return View();
                    }
                }

            }

            return View("Results", doc);
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


// #################### OLD CODE #########################
//namespace _1Lieferschein.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;

//        public HomeController(ILogger<HomeController> logger)
//        {
//            _logger = logger;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Results()
//        {
//            return View();
//        }


//        [HttpPost("Results")]
//        public async Task<ActionResult> UploadFile(IFormFile[] fileUpload)
//        {
//            //DeliveryNoteUbl4 returnModel = new DeliveryNoteUbl4();
//            DeliveryNote returnModel = new DeliveryNote();

//            //check how many xmls are uploaded
//            for (int i = 0; i < fileUpload.Length; i++)
//            {
//                IFormFile uploadFile = fileUpload[i];

//                String contentType = uploadFile.ContentType;
//                bool IsValidDoc = false;
//                if (!string.IsNullOrEmpty(contentType) && contentType.Equals(MediaTypeNames.Text.Xml))
//                {
//                    IsValidDoc = true;
//                }

//                if (IsValidDoc == false)
//                {
//                    ViewBag.Message = "Fehler: Ungültiges Dokument";
//                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//                }

//                using (var ms = new MemoryStream())
//                {
//                    try
//                    {
//                        DespatchAdvice despatchAdvice = Common.Deserialize<DespatchAdvice>(await Common.ReadAsStringAsync(uploadFile));
//                        ViewBag.Message = "XML erfolgreich hochgeladen.";

//                        switch (i)
//                        {
//                            case 0:
//                                returnModel.DespatchAdvice1 = despatchAdvice;
//                                break;
//                            case 1:
//                                returnModel.DespatchAdvice2 = despatchAdvice;
//                                break;
//                        }
//                    }
//                    catch (XmlException xmlException)
//                    {
//                        ViewBag.Message = "Fehler: " + xmlException.Message + ", inner-exception: " + xmlException?.InnerException?.Message + ", strack-trace: " + xmlException.StackTrace;
//                        return View();
//                    }
//                }

//            }

//            return View("Results", returnModel);
//        }

//        private byte[] ConvertToByte(IFormFile file)
//        {
//            using (var memoryStream = new MemoryStream())
//            {
//                file.OpenReadStream().CopyTo(memoryStream);
//                return memoryStream.ToArray();
//            }
//        }

//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }

//    }
//}
