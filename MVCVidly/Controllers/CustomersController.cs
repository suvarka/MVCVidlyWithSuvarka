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
            //var CustomerList = _context.Customers.Include(c => c.MembershipType).ToList();
            return View();
        }

        public ActionResult CreateCustomer()
        {
            var membershipTypeList = _context.MembershipTypes.ToList();
            CustomerFormViewModel newCustomerViewModel = new CustomerFormViewModel()
            {
                MembershipTypes = membershipTypeList,
                Customer = new Customer()
            };
            return View("CustomerForm", newCustomerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var membershipTypeList = _context.MembershipTypes.ToList();
                CustomerFormViewModel newCustomerViewModel = new CustomerFormViewModel()
                {
                    MembershipTypes = membershipTypeList,
                    Customer = customer
                };
                return View("CustomerForm", newCustomerViewModel);
            }

            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerDetails = _context.Customers.Where(c => c.Id == customer.Id).FirstOrDefault();
                customerDetails.Name = customer.Name;
                customerDetails.MembershipTypeId = customer.MembershipTypeId;
                customerDetails.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerDetails.BirthDate = customer.BirthDate;                
            }
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.Where(c => c.Id == id).FirstOrDefault();
            CustomerFormViewModel newCustomerViewModel = new CustomerFormViewModel()
            {
                MembershipTypes = _context.MembershipTypes.ToList(),
                Customer = customer
            };

            return View("CustomerForm", newCustomerViewModel);
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