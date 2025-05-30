using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricState.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage ="Product is Required")]
        [StringLength(100,ErrorMessage ="Product Name cannot exceed 100 characters")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Display(Name = "Price")]
        [Precision(18, 2)]
        public decimal Price { get; set; }


        [Display(Name ="Image")]
        [StringLength(500)]
        public string? ImageURL { get; set; }

        [Required(ErrorMessage = "stock is Required")]
        [Range(0, int.MaxValue, ErrorMessage = "must be greater than 0")]
        [Display(Name = "Stock")]
        public int Stock { get; set; }

        //Time
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        //ForeignKey 

        [Required(ErrorMessage = "Category is Required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        // Navigation property
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

    }
}
