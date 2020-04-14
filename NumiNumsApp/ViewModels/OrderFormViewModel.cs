using System.Collections.Generic;
using NumiNumsApp.Models;

namespace NumiNumsApp.ViewModels
{
    public class OrderFormViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
        public Order Order { get; set; }
        public OrderDetail OrderDetail { get; set; }

    }
}