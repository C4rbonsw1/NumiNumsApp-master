using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NumiNumsApp.Models;
using NumiNumsApp.ViewModels;


namespace NumiNumsApp.Controllers
{
    public class OrderFormController : Controller
    {
        private ApplicationDbContext storeDB = new ApplicationDbContext();
        // GET: OrderForm
        public ActionResult OrderForm()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var orderFormViewModel = new OrderFormViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal(),
            };
            return View(orderFormViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> OrderForm([Bind(Include = "OrderId,CartId,FirstName,LastName,Address,City,PostalCode,Phone,Email,Total,OrderDate,OrderDetails")]Order order)
        {
            order.OrderDate = DateTime.Now;
            //Save Order
            storeDB.Orders.Add(order);
            storeDB.SaveChanges();

            //cart.AddOrder(order);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.CreateOrder(order);

            var orderDetails = storeDB.OrderDetails.Include(p => p.Product).Where(od => od.OrderId == order.OrderId).ToList();
            order.OrderDetails = orderDetails;

            var body = "<p>Order From: {0} ({1})</p><p>Order:</p><p>{2}</p>";

            DataTable dt = new DataTable();

            dt.Columns.Add("Name");
            dt.Columns.Add("Price");
            dt.Columns.Add("Quantity");
            foreach (var item in order.OrderDetails)
            {
                var row = dt.NewRow();

                row["Name"] = item.Product.Name;
                row["Quantity"] = item.Quantity.ToString();
                //row["Price"] = Convert.ToString(item.Product.Price);

                dt.Rows.Add(row); 
            }

            foreach (DataRow dr in dt.Rows) 
            {
                body += "<tr>";
                body += "<td>" + dr[0] + "</td>";
                body += "<td>" + dr[1] + "</td>";
                //body += "<td>" + String.Format("{0:c}", dr[2]) + "</td>";
                body += "</tr>";
            }
            body += "</table>";

            var sender = new MailAddress("danielgeorgeswan@gmail.com");

            var message = new MailMessage
            {
                Sender = sender
            };
            message.To.Add(new MailAddress("swanmunky1@gmail.com"));  // replace with valid value 
            message.From = sender;  // replace with valid value
            message.Subject = "New Order";
            message.Body = string.Format(body, order.FirstName, order.LastName, order.Email);
            message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.sendgrid.net";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("apikey", "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"); // Enter seders User name and password  
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(message);

            return RedirectToAction("Complete", new { id = order.OrderId });
        }




        public ActionResult Complete(int id)
        {
            string cartId = "";
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cartId = cart.GetCartId(this.HttpContext);
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id);

            Order order = new Order();
            order = storeDB.Orders.SingleOrDefault(i => i.OrderId == id);

            if (isValid)
            {
                return View(order);
            }
            else
            {
                return View("Error");
            }
        }
    }
}