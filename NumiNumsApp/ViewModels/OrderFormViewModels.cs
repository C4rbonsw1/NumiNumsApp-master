﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NumiNumsApp.Models;

namespace NumiNumsApp.ViewModels
{
    public class OrderFormViewModels
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
        public Order Order { get; set; }
    }
}