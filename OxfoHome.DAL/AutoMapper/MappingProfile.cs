using AutoMapper;
using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;

namespace OxfoHome.DAL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Payment, PaymentDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Supplier, SupplierDTO>().ReverseMap();
            CreateMap<PayingEntity, PayeeDTO>().ReverseMap();
            CreateMap<RawMaterial, RawMaterialDTO>().ReverseMap();
            CreateMap<DyeingVendor, DyeingVendorDTO>().ReverseMap();
            CreateMap<SharingVendor, SharingVendorDTO>().ReverseMap();
            CreateMap<CustomerOrder, CustomerOrderDTO>().ReverseMap();
            CreateMap<DyeingProcess, DyeingProcessDTO>().ReverseMap();
            CreateMap<CompanyProfile, CompanyProfileDTO>().ReverseMap();
            CreateMap<AccountPayment, AccountPaymentDTO>().ReverseMap();
            CreateMap<DyeingColorRate, DyeingColorRateDTO>().ReverseMap();
            CreateMap<RawMaterialOrder, RawMaterialOrderDTO>().ReverseMap();
            CreateMap<CustomerOrderDetail, CustomerOrderDetailDTO>().ReverseMap();
            CreateMap<RawMaterialOrderDetail, RawMaterialOrderDetailDTO>().ReverseMap();
        }
    }
}
