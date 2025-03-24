using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Labb02_Webbutveckling.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
        [JsonIgnore]
        public List<ShoppingCart> ShoppingCarts { get; set; } = new();
    }

}
