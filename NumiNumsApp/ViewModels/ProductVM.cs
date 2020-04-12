using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NumiNumsApp.Models;

namespace NumiNumsApp.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }

        [DataType(DataType.Upload)]
        public string File { get; set; }

        public ProductType ProductType { get; set; }
        public IEnumerable<SelectListItem> ListProductType { get; set; }
    }
} 