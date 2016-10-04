using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Lab24.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {

            return View();
        }

        // GET: Customer/Details/5
        public ActionResult List()
        {
            NorthwindEntities dbcontext = new NorthwindEntities();
            List<Customer> customers = dbcontext.Customers.ToList();

            return View(customers);
        }

      
        public ActionResult Add( )
        {
                    
       
            return View();
        }

        [HttpPost]
        public ActionResult Add(Customer customer)
        {
            try
            {
                NorthwindEntities dbcontext = new NorthwindEntities();
                Customer customers = dbcontext.Customers.Add(customer);
                dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View();
            }
            
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(string id)
        {
            NorthwindEntities dbcontext = new NorthwindEntities();
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Customer customers = dbcontext.Customers.Find(id);
           
            //if (customers == null)
            //{
            //    return HttpNotFound();
            //}
            return View(customers);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Customer customers)
        {
            try
            {
                NorthwindEntities dbcontext = new NorthwindEntities();
              
                if (ModelState.IsValid)
                {
                    dbcontext.Entry(customers).State = EntityState.Modified;
                    dbcontext.SaveChanges();
                   
                }
               

                return RedirectToAction("List");
            }
            catch
            {
                return View(customers);
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(string id)
        {
            NorthwindEntities dbcontext = new NorthwindEntities();
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Customer customers = dbcontext.Customers.Find(id);

            //if (customers == null)
            //{
            //    return HttpNotFound();
            //}
            return View(customers);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Customer customers)
        {
            try
            {
                NorthwindEntities dbcontext = new NorthwindEntities();
                Customer cust = dbcontext.Customers.Find(id);
                Order_Detail details = new Order_Detail();

               var orders = dbcontext.Orders.Where(x => x.CustomerID == id);

                var orderDetailRecords = from order in dbcontext.Orders
                                         join orderDetail in dbcontext.Order_Details on order.OrderID equals orderDetail.OrderID
                                         select orderDetail;
           
                dbcontext.Order_Details.RemoveRange(orderDetailRecords.ToList());

                dbcontext.Orders.RemoveRange(orders.ToList());
                dbcontext.Customers.Remove(cust);
              
                    dbcontext.SaveChanges();
              
               
                return RedirectToAction("List");
            }
            catch(Exception ex)
            {
                ViewBag.ExceptionMessage = ex.Message+ex.InnerException.Message;
                return View(customers);
            }
        }
    }
}
