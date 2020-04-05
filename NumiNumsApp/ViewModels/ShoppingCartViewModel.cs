using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NumiNumsApp.Models;

namespace NumiNumsApp.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
        public List<Product> Products { get; set; }
    }
}