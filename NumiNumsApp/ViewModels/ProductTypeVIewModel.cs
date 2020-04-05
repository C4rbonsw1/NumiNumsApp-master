using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NumiNumsApp.Models;

namespace NumiNumsApp.ViewModels
{
    public class ProductTypeVIewModel
    {
        public Product Product { get; set; }
        public ProductType ProductType { get; set; }
        public IEnumerable<SelectListItem> ListProductType { get; set; }
    }
}