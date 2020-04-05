using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NumiNumsApp.Models
{
    public class ProductType
    {
        [Key]
        public int ProductTypeId { get; set; }
        public string Name { get; set; }
        public double TypePrice { get; set; }
    }
}