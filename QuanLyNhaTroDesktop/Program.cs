using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Services;
using QuanLyNhaTroDesktop.Forms;
using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop;

static class Program
{
    public static IServiceProvider Services { get; private set; } = null!;

    [STAThread]
    static void Main(string[] args)
    {
        // Đảm bảo app nhận biết DPI màn hình (chống cắt chữ trên scale 125%/150%)
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        ApplicationConfiguration.Initialize();
        Dpi.Initialize();   // đo hệ số scale để tự nhân kích thước control

        // Mọi lỗi không lường trước (NullReference, khoá dữ liệu...) được GHI ĐẦY ĐỦ stack trace
        // ra file loi-ung-dung.log cạnh file .exe, thay vì chỉ hiện hộp thoại chung chung
        // "Object reference not set to an instance of an object" như mặc định của .NET.
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) => WriteCrashLog(e.Exception, "giao diện");
        AppDomain.CurrentDomain.UnhandledException += (_, e) => WriteCrashLog(e.ExceptionObject as Exception, "hệ thống");

        // ==== Dựng DI container (giống Program.cs của bản web) ====
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);

        // Ở các chế độ chạy thử (--form, --dump-layout, --preview, --designer-check)
        // thì tắt bớt log SQL cho dễ nhìn kết quả.
        var quietMode = args.Any(a => a is "--form" or "--dump-layout" or "--preview" or "--designer-check");
        services.AddLogging(b =>
        {
            b.AddConsole();
            if (quietMode && !args.Contains("--verbose-sql")) b.SetMinimumLevel(LogLevel.Warning);
        });

        // Database: giống web — sqlite (demo) hoặc sqlserver, chọn qua appsettings.json
        var dbProvider = configuration["Database:Provider"] ?? "sqlserver";
        // LƯU Ý quan trọng: mỗi lần lấy service ra là một DbContext RIÊNG (Transient).
        // Bản web dùng Scoped vì mỗi request HTTP có một vòng đời riêng; app desktop thì
        // tất cả cửa sổ dùng chung DI container, nếu để Scoped (một DbContext cho cả app)
        // thì hai thao tác chồng nhau (bấm tab nhanh, mở cửa sổ khi danh sách đang tải...) sẽ
        // báo lỗi "A second operation was started on this context instance" — SQL Server
        // chậm hơn SQLite nên lỗi này lộ rõ. Các Service đều đọc lại entity theo Id rồi gán
        // từng trường (không truyền entity rời), nên dùng DbContext riêng là an toàn.
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (dbProvider.Equals("sqlite", StringComparison.OrdinalIgnoreCase))
                options.UseSqlite(configuration.GetConnectionString("DemoSqlite"));
            else
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }, ServiceLifetime.Transient, ServiceLifetime.Singleton);

        // Identity (DbInitializer dùng UserManager để tạo tài khoản chủ trọ)
        services.AddIdentityCore<QuanLyNhaTro.Models.AppUser>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
            options.Lockout.MaxFailedAccessAttempts = 5;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>();

        // Business services — tái sử dụng nguyên vẹn từ bản web
        services.AddTransient<IRoomService, RoomService>();
        services.AddTransient<ITenantService, TenantService>();
        services.AddTransient<IContractService, ContractService>();
        services.AddTransient<IUtilityService, UtilityService>();
        services.AddTransient<IServiceService, ServiceService>();
        services.AddTransient<IInvoiceService, InvoiceService>();
        services.AddTransient<IPaymentService, PaymentService>();
        services.AddTransient<IMoveOutService, MoveOutService>();
        services.AddTransient<IReportService, ReportService>();
        services.AddTransient<IDashboardService, DashboardService>();
        services.AddTransient<IExportService, ExportService>();

        Services = services.BuildServiceProvider();

        // ==== Khởi tạo database + seed lần đầu chạy (dùng lại DbInitializer của web) ====
        using (var scope = Services.CreateScope())
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>()
                .CreateLogger("QuanLyNhaTroDesktop");
            try
            {
                DbInitializer.InitializeAsync(scope.ServiceProvider, logger).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Lỗi khởi tạo database.");
                MessageBox.Show(
                    "Không khởi tạo được database:\n" + ex.Message +
                    "\n\nKiểm tra connection string trong appsettings.json.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // ==== Chế độ kiểm tra layout (dùng khi phát triển): ghi toạ độ control ra file ====
        if (args.Contains("--dump-layout"))
        {
            LayoutDump.RunAll();
            return;
        }

        // ==== Chế độ kiểm tra form có mở được bằng Visual Studio Designer không ====
        if (args.Contains("--designer-check"))
        {
            DesignerCheck.Run();
            return;
        }

        // ==== Chế độ xuất ảnh xem trước mọi giao diện (mở preview/index.html để xem) ====
        if (args.Contains("--preview"))
        {
            FormPreview.RunAll();
            return;
        }

        // ==== Chế độ chạy THỬ riêng 1 cửa sổ, bỏ qua Đăng nhập: --form traphong ====
        if (TestForm.Run(args)) return;

        try
        {
            // ==== Đăng nhập rồi mở cửa sổ chính ====
            using (var login = new LoginForm())
            {
                if (login.ShowDialog() != DialogResult.OK)
                    return; // người dùng đóng cửa sổ đăng nhập → thoát
            }

            Application.Run(new MainForm());
        }
        catch (Exception ex)
        {
            WriteCrashLog(ex, "chương trình chính");
        }
    }

    /// <summary>
    /// Ghi lỗi (kèm stack trace) ra file loi-ung-dung.log cạnh file .exe rồi báo đường dẫn cho
    /// người dùng. Có file này thì việc tìm nguyên nhân lỗi nhanh hơn nhiều so với đoán.
    /// </summary>
    private static void WriteCrashLog(Exception? ex, string nguon)
    {
        string path;
        try
        {
            path = Path.Combine(AppContext.BaseDirectory, "loi-ung-dung.log");
            File.AppendAllText(path,
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Lỗi ({nguon}){Environment.NewLine}" +
                $"{ex}{Environment.NewLine}{Environment.NewLine}");
        }
        catch
        {
            path = "(không ghi được file log)";
        }

        try
        {
            Msg.Error(null,
                "Ứng dụng gặp lỗi không mong muốn.\n\n" +
                (ex?.Message ?? "(không có mô tả)") +
                "\n\nChi tiết đã ghi vào file:\n" + path);
        }
        catch
        {
            // đang xử lý lỗi thì không để phát sinh lỗi thứ hai
        }
    }
}
