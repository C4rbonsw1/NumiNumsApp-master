using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NumiNumsApp.Models
{
    public class ImageUpload
    {
        [DataType(DataType.Upload)]
        public string file { get; set; }
    }
}