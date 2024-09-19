using OxfoHome.Data.Entities;

namespace OxfoHome.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IRepository<Account> Accounts { get; }
        public IRepository<Payment> Payments { get; }
        public IRepository<PayingEntity> PayingEntities { get; }
        public IRepository<AccountPayment> AccountPayments { get; }
        public IRepository<CompanyProfile> CompanyProfiles { get; }
        public IRepository<Customer> Customers { get; }
        public IRepository<CustomerOrder> CustomerOrders { get; }
        public IRepository<CustomerOrderDetail> CustomerOrderDetails { get; }
        public IRepository<DyeingColorRate> DyeingColorRates { get; }
        public IRepository<DyeingProcess> DyeingProcess { get; }
        public IRepository<DyeingVendor> DyeingVendors { get; }
        public IRepository<SharingVendor> SharingVendors { get; }
        public IRepository<Product> Products { get; }
        public IRepository<RawMaterial> RawMaterials { get; }
        public IRepository<RawMaterialOrder> RawMaterialOrders { get; }
        public IRepository<RawMaterialOrderDetail> RawMaterialOrderDetails { get; }
        public IRepository<ActivityLog> ActivityLogs { get; }
        public IRepository<ErrorLog> ErrorLogs { get; }
        public IRepository<Supplier> Suppliers { get; }
        public IRepository<User> Users { get; }
        public IRepository<Role> Roles { get; }

        public UnitOfWork(ApplicationDbContext context, IRepository<Account> accounts,
            IRepository<AccountPayment> accountPayments, IRepository<CompanyProfile> companyProfiles,
            IRepository<Customer> customers, IRepository<CustomerOrder> customerOrders,
            IRepository<CustomerOrderDetail> customerOrderDetails, IRepository<DyeingColorRate> dyeingColorRates,
            IRepository<DyeingProcess> dyeingProcess, IRepository<DyeingVendor> dyeingVendors,
            IRepository<Product> products, IRepository<RawMaterial> rawMaterials,
            IRepository<RawMaterialOrder> rawMaterialOrders, IRepository<RawMaterialOrderDetail> rawMaterialOrderDetails,
            IRepository<Supplier> suppliers, IRepository<User> users,
            IRepository<ActivityLog> activityLogs, IRepository<ErrorLog> errorLogs, IRepository<Role> roles,
            IRepository<SharingVendor> sharingVendors, IRepository<PayingEntity> payingEntites,
            IRepository<Payment> payments)
        {
            _context = context;
            Accounts = accounts;
            AccountPayments = accountPayments;
            CompanyProfiles = companyProfiles;
            Customers = customers;
            CustomerOrders = customerOrders;
            CustomerOrderDetails = customerOrderDetails;
            DyeingColorRates = dyeingColorRates;
            DyeingProcess = dyeingProcess;
            DyeingVendors = dyeingVendors;
            Products = products;
            RawMaterials = rawMaterials;
            RawMaterialOrders = rawMaterialOrders;
            RawMaterialOrderDetails = rawMaterialOrderDetails;
            Suppliers = suppliers;
            Users = users;
            ActivityLogs = activityLogs;
            ErrorLogs = errorLogs;
            Roles = roles;
            SharingVendors = sharingVendors;
            PayingEntities = payingEntites;
            Payments = payments;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
