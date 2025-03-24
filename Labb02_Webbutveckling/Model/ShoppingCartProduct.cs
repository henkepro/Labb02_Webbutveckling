using Labb02_Webbutveckling.Components;
using Labb02_Webbutveckling.Model;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Labb02_Webbutveckling.Model
{
    public class ShoppingCartProduct
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        [JsonIgnore]
        public ShoppingCart ShoppingCart { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}