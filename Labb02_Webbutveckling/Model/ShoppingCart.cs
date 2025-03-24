using System.Text.Json.Serialization;

namespace Labb02_Webbutveckling.Model
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }

        public bool ActiveCart { get; set; } = true;
        public List<ShoppingCartProduct> ShoppingCartProducts { get; set; } = new();
    }

}
