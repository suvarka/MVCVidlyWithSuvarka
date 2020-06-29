using AutoMapper;
using MVCVidly.Dtos;
using MVCVidly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVCVidly.Controllers.API
{
    public class CustomerController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //Get /api/Customer
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        //Get /api/Customer/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer= _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }

        //Post /api/Customer
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (customerDto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var customer= Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.Id = customer.Id;
            //api/Customer/10
            return Created(new Uri(Request.RequestUri +"/" +customer.Id), customerDto);
        }

        //Put /api/Customer/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (customerDto == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.FirstOrDefault(c => c.Id == id);

            Mapper.Map(customerDto, customerInDb);
            //customerInDb.Name = customerDto.Name;
            //customerInDb.MembershipTypeId = customerDto.MembershipTypeId;
            //customerInDb.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;
            //customerInDb.BirthDate = customerDto.BirthDate;
            _context.SaveChanges();
        }

        //Delete /api/Customer/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb=_context.Customers.FirstOrDefault(c => c.Id == id);
            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }

    }
}
