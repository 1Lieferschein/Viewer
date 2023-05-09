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
using System.Collections;
using System.Runtime.CompilerServices;

namespace _1Lieferschein.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private List<String> unknownInformation;

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
            DeliveryNoteUbl4 returnModel = new DeliveryNoteUbl4();

            unknownInformation = new List<String>();

            for (int i = 0; i < fileUpload.Length; i++)
            {
                IFormFile uploadFile = fileUpload[i];

                String contentType = uploadFile.ContentType;
                

                if (string.IsNullOrEmpty(contentType) && contentType.Equals(MediaTypeNames.Text.Xml))
                {
                    ViewBag.Message = "Fehler: Ungültiges Dokument";
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }

                using (var ms = new MemoryStream())
                {
                    try
                    {
                        // initialise deserializer
                        XmlSerializer ser = new XmlSerializer(typeof(DespatchAdvice));

                        // add event for handling unknown objects
                        ser.UnknownElement += new XmlElementEventHandler(Serializer_UnknownElement);

                        // A FileStream is necessary to read the XML document.
                        await uploadFile.CopyToAsync(ms);
                        ms.Seek(0, SeekOrigin.Begin);


                        DespatchAdvice des = (DespatchAdvice)ser.Deserialize(ms);
                        des.unknownInformation = unknownInformation;
                        // fs.Close();
                        ms.Close();
                        // readAsStringAsync --> Serialisiert den HTTP-Inhalt in eine Zeichenfolge als asynchroner Vorgang                       

                        //DespatchAdvice despatchAdvice = Common.Deserialize<DespatchAdvice>(await Common.ReadAsStringAsync(uploadFile));
                        ViewBag.Message = "XML erfolgreich hochgeladen.";

                        switch (i)
                        {
                            case 0:
                                returnModel.DespatchAdvice1 = des;
                                break;
                            case 1:
                                returnModel.DespatchAdvice2 = des;
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
            unknownInformation.Add("In Zeile Nr. " + e.LineNumber + ": " + e.Element.Name + " " + e.Element.InnerXml);
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