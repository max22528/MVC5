using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CRUDController : Controller
    {
        // GET: CRUD
        public ActionResult Index(string keyword)
        {
    
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "A", Value = "A" });
            items.Add(new SelectListItem { Text = "B", Value = "B" });
            items.Add(new SelectListItem { Text = "C", Value = "C", Selected = true });

            ViewBag.ProductNameStartsWith = items;
            //&& p.ProductName.StartsWith()

            var db = new FabricsEntities();
            var data = db.Product.Where(p => p.ProductName.StartsWith(keyword));

            return View(data);
        }

        // GET: CRUD/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CRUD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CRUD/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var db = new FabricsEntities();

                var product = new Product();

                product.ProductName = collection["ProductName"];
                product.Price = Convert.ToDecimal(collection["Price"]);
                product.Stock = Convert.ToDecimal(collection["Stock"]);
                product.Active = true;

                db.Product.Add(product);
                db.SaveChanges();
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult BatchUpdate()
        {
            var db = new FabricsEntities();
            var data = db.Product.Where(p => p.ProductName.StartsWith("A"));

            foreach (var item in data)
            {
                item.Price = item.Price * 2;
            }

            try
           {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }

            return RedirectToAction("Index");
        }

        // GET: CRUD/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CRUD/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CRUD/Delete/5
        public ActionResult Delete(int id)
        {
            var db = new FabricsEntities();

            var client = db.Client.Find(id);

            if (client != null)
            {
                foreach (var item in client.Order.ToList())
                {
                    db.OrderLine.RemoveRange(item.OrderLine.ToList());
                }

                db.Order.RemoveRange(client.Order.ToList());

                db.Client.Remove(client);

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        // POST: CRUD/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
