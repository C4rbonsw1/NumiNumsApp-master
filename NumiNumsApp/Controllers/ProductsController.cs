using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NumiNumsApp.Models;
using NumiNumsApp.ViewModels;

namespace NumiNumsApp.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Products
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Home/Index");
            }
            var product = db.Products.ToList();
            product = db.Products.Include(x => x.ProductVType).ToList();
            return View(product);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Home/Index");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Home/Index");
            }
            ProductVM prodTypeList = new ProductVM();

            prodTypeList.ListProductType = db.ProductTypes.Select(c => new SelectListItem
            {
                Value = c.ProductTypeId.ToString(),
                Text = c.Name,
                Selected = c.ProductTypeId == c.ProductTypeId
            });
            return View(prodTypeList);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product, File, ProductType, ListProductType")] ProductVM productVM, HttpPostedFileBase file)
        {
            ProductVM viewModel = new ProductVM();
            if (ModelState.IsValid)
            {
                   string savePath = Path.Combine(Server.MapPath("~/Content/Images"), Path.GetFileName(file.FileName));
                   file.SaveAs(savePath);
                   string urlPath = "~/Content/Images/" + file.FileName;
                   if (!System.IO.File.Exists(savePath))
                   {
                       return View("Error, image already exists");
                   }
                   else
                   {
                       ViewBag.SuccessMessage += string.Format("Uploaded!<br />");
                   }
                productVM.Product.ImagePath = urlPath;

                var prodTypeInDb = db.ProductTypes.Where(x => x.ProductTypeId == productVM.ProductType.ProductTypeId).SingleOrDefault();
                productVM.Product.ProductVType = prodTypeInDb;

                 db.Products.Add(productVM.Product);
                 db.SaveChanges();
                 return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Home/Index");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ProductVM prodTypeList = new ProductVM();
            prodTypeList.Product = db.Products.Include(x => x.ProductVType).ToList().Single(x => x.ProductId == id);

            prodTypeList.ListProductType = db.ProductTypes.Select(c => new SelectListItem
            {
                Value = c.ProductTypeId.ToString(),
                Text = c.Name,
                Selected = prodTypeList.Product.ProductVType.ProductTypeId == c.ProductTypeId
            });

            return View(prodTypeList);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product, File, ProductType, ListProductType")] ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                productVM.Product.ProductVType = productVM.ProductType;
                //db.ProductTypes.Attach(productVM.ProductType);
                var prodInDb = db.Products.Include(i => i.ProductVType).SingleOrDefault(i => i.ProductId == productVM.Product.ProductId);
                productVM.Product.ProductVType.ProductTypeId = productVM.ProductType.ProductTypeId;
                if (prodInDb != null)
                {
                    db.Entry(prodInDb).CurrentValues.SetValues(productVM.Product);
                    prodInDb.ProductVType = productVM.ProductType;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productVM);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Home/Index");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
