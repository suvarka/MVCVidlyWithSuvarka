using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCVidly.Models;
using MVCVidly.ViewModel;
using System.Data.Entity;

namespace MVCVidly.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Customers
        public ActionResult Index()
        {
            var CustomerList = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(CustomerList);
        }

        public ActionResult CreateCustomer()
        {
            var membershipTypeList = _context.MembershipTypes.ToList();
            NewCustomerViewModel newCustomerViewModel = new NewCustomerViewModel()
            {
                MembershipTypes = membershipTypeList,
                Customer = new Customer()
            };
            return View(newCustomerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomer(Customer customer)
        {
            if(ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                var membershipTypeList = _context.MembershipTypes.ToList();
                NewCustomerViewModel newCustomerViewModel = new NewCustomerViewModel()
                {
                    MembershipTypes = membershipTypeList,
                    Customer = new Customer()
                };
                return View("CreateCustomer", newCustomerViewModel);
            }
                
        }
        public ActionResult Details(int id)
        {
            var CustomerDetails= _context.Customers.Include(c=>c.MembershipType).Where(c=>c.Id==id).SingleOrDefault();
            return View(CustomerDetails);
        }

        public RedirectToRouteResult Delete(int id)
        {
           var CusDetail= _context.Customers.Where(c => c.Id == id).SingleOrDefault();
            _context.Customers.Remove(CusDetail);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}