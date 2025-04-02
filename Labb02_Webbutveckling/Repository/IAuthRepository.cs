using Labb02_Webbutveckling.Model;

namespace Labb02_Webbutveckling.Repository
{
    public interface IAuthRepository
    {
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task<Customer> GetCustomerByEmailAndPasswordAsync(string email, string password);
    }
}
