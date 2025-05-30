using System.ComponentModel.DataAnnotations;

namespace ElectricState.ViewModels.Supplier
{
    public class SupplierViewModel
    {
        [Required(ErrorMessage = "Supplier Name is Required")]
        [StringLength(100, ErrorMessage = "Supplier Name cannot exceed 100 characters")]
        public string? SupplierName { get; set; }

        [Required(ErrorMessage = "Supplier Address is Required")]
        [StringLength(200, ErrorMessage = "Supplier Address cannot exceed 200 characters")]
        public string? SupplierAddress { get; set; }

        [Required(ErrorMessage = "Supplier Phone is Required")]
        [StringLength(15, ErrorMessage = "Supplier Phone cannot exceed 15 characters")]
        public string? SupplierPhone { get; set; }
    }
}
