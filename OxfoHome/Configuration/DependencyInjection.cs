using OxfoHome.DAL;
using OxfoHome.DAL.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        //generic repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        //services to perform DB operations and other stuff
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IRawMaterialService, RawMaterialService>();
        services.AddScoped<IDyeingService, DyeingService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IHashService, HashService>();
        services.AddScoped<ISharingService, SharingService>();
        services.AddScoped<IPayeeService, PayeeService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IExcelService, ExcelService>();

        services.AddScoped<IAuthService, AuthService>();

        services.AddHttpContextAccessor();

        services.AddAuthorizationCore();

        //encryption service will be added as singleton so there is no issue in encrypting, decrypting data
        //We need to use static encryption keys because we want the URL to work even after the app is restarted
        var encryptionSettings = configuration.GetSection("EncryptionSettings");
        var key = Convert.FromBase64String(encryptionSettings["Key"]);
        var iv = Convert.FromBase64String(encryptionSettings["IV"]);
        services.AddSingleton<IEncryptionService>(new EncryptionService(key, iv));

        return services;
    }
}