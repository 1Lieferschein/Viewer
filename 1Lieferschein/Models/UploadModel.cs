using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace _1Lieferschein.Models
{
    public class UploadModel
    {
        public String FilePath { get; set; }

        public IFormFile UploadFile { get; set; }

        public UploadModel(String FilePath, FormFile UploadFile)
        {
            this.FilePath = FilePath;
            this.UploadFile = UploadFile; 
        }
    }
}
