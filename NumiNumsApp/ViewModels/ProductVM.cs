using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NumiNumsApp.Models;

namespace NumiNumsApp.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }

        [DataType(DataType.Upload)]
        public string File { get; set; }
    }
}