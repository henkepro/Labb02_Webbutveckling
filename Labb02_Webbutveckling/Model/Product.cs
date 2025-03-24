using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Labb02_Webbutveckling.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsEditing { get; set; } = false;
        public bool IsAvailable { get; set; } = true;
        [JsonIgnore]
        public List<ShoppingCartProduct> ShoppingCartProducts { get; set; } = new();
    }

}
