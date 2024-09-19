using OxfoHome.Data.Entities;

namespace OxfoHome.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Account> Accounts { get; }
        IRepository<Payment> Payments { get; }
        IRepository<PayingEntity> PayingEntities { get; }
        IRepository<AccountPayment> AccountPayments { get; }
        IRepository<CompanyProfile> CompanyProfiles { get; }
        IRepository<Customer> Customers { get; }
        IRepository<CustomerOrder> CustomerOrders { get; }
        IRepository<CustomerOrderDetail> CustomerOrderDetails { get; }
        IRepository<DyeingColorRate> DyeingColorRates { get; }
        IRepository<DyeingProcess> DyeingProcess { get; }
        IRepository<DyeingVendor> DyeingVendors { get; }
        IRepository<SharingVendor> SharingVendors { get; }
        IRepository<Product> Products { get; }
        IRepository<RawMaterial> RawMaterials { get; }
        IRepository<RawMaterialOrder> RawMaterialOrders { get; }
        IRepository<RawMaterialOrderDetail> RawMaterialOrderDetails { get; }
        IRepository<Supplier> Suppliers { get; }
        IRepository<User> Users { get; }
        IRepository<Role> Roles { get; }
        IRepository<ActivityLog> ActivityLogs { get; }
        IRepository<ErrorLog> ErrorLogs { get; }
        Task<int> SaveAsync();
    }
}
