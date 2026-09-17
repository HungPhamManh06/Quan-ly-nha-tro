using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

// Database: Microsoft SQL Server + EF Core (mặc định)
// Chế độ demo/preview: đặt Database:Provider=sqlite để chạy không cần SQL Server
var dbProvider = configuration["Database:Provider"] ?? "sqlserver";
services.AddDbContext<ApplicationDbContext>(options =>
{
    if (dbProvider.Equals("sqlite", StringComparison.OrdinalIgnoreCase))
        options.UseSqlite(configuration.GetConnectionString("DemoSqlite"));
    else
    {
        // Connection string có thể đặt qua biến môi trường ConnectionStrings__DefaultConnection
        // (Render/Docker sẽ truyền vào, ghi đè giá trị trong appsettings.json).
        // EnableRetryOnFailure: tự thử lại khi mất kết nối tạm thời — cần thiết khi dùng
        // SQL Server trên cloud (Azure SQL / SQL Server trên Render).
        options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection"),
            sqlOptions => sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null));
    }
});

// Authentication - chỉ một loại người dùng: Chủ trọ
services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = false;
        // Chủ trọ là người dùng duy nhất, giữ chính sách vừa phải nhưng vẫn an toàn
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireDigit = false;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Home/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
});

services.AddControllersWithViews();

// Business services
services.AddScoped<IRoomService, RoomService>();
services.AddScoped<ITenantService, TenantService>();
services.AddScoped<IContractService, ContractService>();
services.AddScoped<IUtilityService, UtilityService>();
services.AddScoped<IServiceService, ServiceService>();
services.AddScoped<IInvoiceService, InvoiceService>();
services.AddScoped<IPaymentService, PaymentService>();
services.AddScoped<IMoveOutService, MoveOutService>();
services.AddScoped<IReportService, ReportService>();
services.AddScoped<IDashboardService, DashboardService>();
services.AddScoped<IExportService, ExportService>();

var app = builder.Build();

// Khởi tạo database: tự động migrate + seed dữ liệu mẫu lần đầu chạy
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        await DbInitializer.InitializeAsync(scope.ServiceProvider, logger);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Lỗi khởi tạo database. Kiểm tra connection string trong appsettings.json và đảm bảo SQL Server đang chạy.");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
