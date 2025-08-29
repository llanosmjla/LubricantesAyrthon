using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LubricantesAyrthonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        // Dummy in-memory list for demonstration
        private static List<Customer> customers = new List<Customer>();

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAll()
        {
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetById(int id)
        {
            var customer = customers.Find(c => c.Id == id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public ActionResult<Customer> Create(Customer customer)
        {
            customer.Id = customers.Count + 1;
            customers.Add(customer);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Customer updatedCustomer)
        {
            var customer = customers.Find(c => c.Id == id);
            if (customer == null)
                return NotFound();

            customer.Name = updatedCustomer.Name;
            customer.Email = updatedCustomer.Email;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = customers.Find(c => c.Id == id);
            if (customer == null)
                return NotFound();

            customers.Remove(customer);
            return NoContent();
        }
    }

    // Simple Customer model for demonstration
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}