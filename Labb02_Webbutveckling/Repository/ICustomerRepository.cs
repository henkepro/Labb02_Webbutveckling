using Labb02_Webbutveckling.Model;

namespace Labb02_Webbutveckling.Repository
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> SearchCustomersByEmailAsync(string email);
        Task<Customer> GetCustomerByIdAsync(int id);
        Task UpdateCustomerAsync(Customer customer);
        Task RemoveCustomerAsync(Customer customer);
        Task<List<Customer>> GetAllCustomersAsync();
        Task AddCustomerAsync(Customer customer);

    }
}
