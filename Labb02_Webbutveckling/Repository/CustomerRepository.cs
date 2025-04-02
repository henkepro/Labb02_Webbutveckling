using Labb02_Webbutveckling.Model;
using Microsoft.EntityFrameworkCore;

namespace Labb02_Webbutveckling.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MyContext _dbContext;

        public CustomerRepository(MyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _dbContext.Customers.FindAsync(id);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<Customer>> SearchCustomersByEmailAsync(string email)
        {
            return await _dbContext.Customers
                .Where(c => c.Email.ToLower().Contains(email.ToLower()))
                .ToListAsync();
        }
        public async Task RemoveCustomerAsync(Customer customer)
        {
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }
        public async Task AddCustomerAsync(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
        }
    }
}
