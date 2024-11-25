using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shop_app.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        //Navigation property
        public Customer? Customer { get; set; }
        //Navigation property
        public ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
