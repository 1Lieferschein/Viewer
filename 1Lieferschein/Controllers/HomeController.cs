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
using System.Text.RegularExpressions;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;

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
//        public async Task<ActionResult> UploadFile(IFormFile fileUpload)
//        {
//            var doc = new XmlDocument();

//            //check how many xmls are uploaded
//            for (int i = 0; i < fileUpload.Length; i++)
//            {
//                IFormFile uploadFile = fileUpload;

//                //check if the uploaded file is a xml
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
//                        var serializer = new XmlSerializer(typeof(DespatchAdvice));
//                        var despatchAdvice = serializer.Deserialize(fileUpload.OpenReadStream());

//                        doc.LoadXml((string)despatchAdvice);


//                        //doc.LoadXml("<book genre='novel' ISBN='1-861001-57-5'>" +
//                        //  "<title>Pride And Prejudice</title>" +
//                        //  "</book>");


//                        //XmlNode root = doc.FirstChild;
//                        //Console.WriteLine(doc.FirstChild);



//                        //Alter Code
//                        //DespatchAdvice despatchAdvice = Common.Deserialize<DespatchAdvice>(await Common.ReadAsStringAsync(uploadFile));
//                        //ViewBag.Message = "XML erfolgreich hochgeladen.";

//                        //var despatch = new DespatchAdvice();
//                        //XmlDocument j = despatch;

//                        //switch (i)
//                        //{
//                        //    case 0:
//                        //        returnModel.DespatchAdvice1 = despatchAdvice;
//                        //        break;
//                        //    case 1:
//                        //        returnModel.DespatchAdvice2 = despatchAdvice;
//                        //        break;
//                        //}



//                    }
//                    catch (XmlException xmlException)
//                    {
//                        ViewBag.Message = "Fehler: " + xmlException.Message + ", inner-exception: " + xmlException?.InnerException?.Message + ", strack-trace: " + xmlException.StackTrace;
//                        return View();
//                    }
//                }

//            }

//            return View("Results", doc);
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


////}


//#################### OLD CODE #########################
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
            DeliveryNoteUbl4 returnModel = new DeliveryNoteUbl4();
            //DeliveryNote returnModel = new DeliveryNote();



            for (int i = 0; i < fileUpload.Length; i++)
            {
                IFormFile uploadFile = fileUpload[i];

                String contentType = uploadFile.ContentType;
                // RR: redundant?
                //bool IsValidDoc = false;
                //if (!string.IsNullOrEmpty(contentType) && contentType.Equals(MediaTypeNames.Text.Xml))
                //{
                //    IsValidDoc = true;
                //}

                //if (IsValidDoc == false)
                //{
                //    ViewBag.Message = "Fehler: Ungültiges Dokument";
                //    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                //}

                if (string.IsNullOrEmpty(contentType) && contentType.Equals(MediaTypeNames.Text.Xml))
                {
                    ViewBag.Message = "Fehler: Ungültiges Dokument";
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }

                using (var ms = new MemoryStream())
                {
                    try
                    {
                        // initialisiere deserializer
                        XmlSerializer ser = new XmlSerializer(typeof(DespatchAdvice));

                        // füge Event für Behandlung unbekannter Objekte hinzu
                        ser.UnknownElement += new XmlElementEventHandler(Serializer_UnknownElement);

                        // A FileStream is necessary to read the XML document.
                        //string path = @"C:\Users\Roentgen\Desktop\Rösch\Viewer\Samples\Sample2.xml";
                        string path = @"C:\Projekte\1Lieferschein\Sample1.xml";
                        FileStream fs = new FileStream(path, FileMode.Open);
                        DespatchAdvice des = (DespatchAdvice)ser.Deserialize(fs);
                        fs.Close();

                        // readAsStringAsync --> Serialisiert den HTTP-Inhalt in eine Zeichenfolge als asynchroner Vorgang                       

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

            return View("Results", returnModel);
        }

        private void Serializer_UnknownElement(object sender, XmlElementEventArgs e)
        {
            // JS: Hier müssten die Daten an die Stringliste des Despatchadvices drangehängt werden
            Console.WriteLine("Unknown Element");
            Console.WriteLine("\t" + e.Element.Name + " " + e.Element.InnerXml);
            Console.WriteLine("\t LineNumber: " + e.LineNumber);
            Console.WriteLine("\t LinePosition: " + e.LinePosition);
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