using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using StoreWebApi.Interfaces;
using StoreWebApi.Models;
using System.Net;
using System.Xml.Linq;

namespace StoreWebApi.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly StoreDbContext _dbContext;

        public CustomerService(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _dbContext.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<Customer?> GetById(int id)
        {
            return await _dbContext.Customers.FindAsync(id);
        }

        public async Task<CustomerDto> Create(CreateCustomerRq customerRq)
        {
            var customer = new Customer()
            {
                Name = customerRq.Name,
                Email = customerRq.Email,
                Phone = customerRq.Phone,
                Address = customerRq.Address
            };

            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            var cus = new CustomerDto()
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address
            };

            return cus;
        }
        
        public async Task Update(int id, CustomerDto customerRq)
        {
            var customer = new Customer()
            {
                Id = customerRq.Id,
                Name = customerRq.Name,
                Email = customerRq.Email,
                Phone = customerRq.Phone,
                Address = customerRq.Address
            };

            _dbContext.Entry(customer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await _dbContext.Customers.FindAsync(id);
            _dbContext.Customers.Remove(item);
            await _dbContext.SaveChangesAsync(); ;
        }
    }
}
