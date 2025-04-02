using Labb02_Webbutveckling.Model;
using Microsoft.EntityFrameworkCore;

namespace Labb02_Webbutveckling.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly MyContext _dbContext;

        public AuthRepository(MyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
        }

        public async Task<Customer> GetCustomerByEmailAndPasswordAsync(string email, string password)
        {
            return await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.Email == email && c.Password == password);
        }
    }
}
