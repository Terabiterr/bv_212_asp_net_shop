using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shop_app.Models
{
    public class OrderProduct
    {
        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [JsonIgnore] //Ignor for error serialization
        public Order? Order { get; set; }
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must have more then zero ...")]
        public int Quantity { get; set; }
    }
}
