using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shop_app.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; } = 0;

        [Required(ErrorMessage ="Name is required")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Name is required ... min: 2, max: 20")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required ...")]
        [Range(0.01, 100000.00, ErrorMessage = "min: 0.01, max: 100000.00")]
        public decimal Price { get; set; } = decimal.Zero;
        [Required(ErrorMessage = "Description is required ...")]

        [StringLength(1024, MinimumLength = 2, ErrorMessage = "min: 2, max: 1024")]
        public string Description { get; set; } = string.Empty;
        [JsonIgnore] //Ignor for error serialization
        //Navigation property
        public ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
