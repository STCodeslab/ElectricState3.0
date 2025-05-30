using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ElectricState.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="Category name is Required")]
        [StringLength(250,ErrorMessage ="Category name cannot exceed 250 characters")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        //TimeOnly 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        // Navigation property
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
