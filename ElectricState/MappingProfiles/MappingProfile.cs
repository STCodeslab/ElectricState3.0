using AutoMapper;
using ElectricState.Models;
using ElectricState.ViewModels.Product;
using ElectricState.ViewModels.Supplier;

namespace ElectricState.MappingProfiles
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductCreateViewModel, Product>();
            CreateMap<Product, ProductCreateViewModel>();

            CreateMap<ProductViewModel, Product>();
            CreateMap<Product, ProductViewModel>();

            CreateMap<Supplier, SupplierViewModel>();
            CreateMap<SupplierViewModel, Supplier>();



        }
    }
}
