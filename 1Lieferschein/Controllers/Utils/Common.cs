using _1Lieferschein.Models.DeliveryNotes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace _1Lieferschein.Controllers.Utils
{
    public static class Common
    {
        public static async Task<String> ReadAsStringAsync(IFormFile uploadFile)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(uploadFile.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(await reader.ReadLineAsync());
            }
            return result.ToString();
        }

        public static T Deserialize<T>(string input) where T : class
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));

                using (StringReader sr = new StringReader(input))
                {       
                    return (T)ser.Deserialize(sr);
                }
            }
            catch (InvalidOperationException invalidOperationException)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
