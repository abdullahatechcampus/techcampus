using DCOps.Web.Data;
using DCOps.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ── Database ─────────────────────────────────────────────────
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure(3)));

// ── Authentication ───────────────────────────────────────────
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });

// ── Authorization ────────────────────────────────────────────
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly",    p => p.RequireRole("admin"));
    options.AddPolicy("ManagerPlus",  p => p.RequireRole("admin", "dc_manager", "it_manager", "department_manager"));
    options.AddPolicy("TechPlus",     p => p.RequireRole("admin", "dc_manager", "it_manager", "technician",
                                                          "facilities_power", "facilities_cooling"));
});

// ── MVC ──────────────────────────────────────────────────────
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<DCOps.Web.Services.AuditActionFilter>();
});

// ── Session ──────────────────────────────────────────────────
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ── Services (DI) ────────────────────────────────────────────
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<ISlaService, SlaService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IAppLocalizationService, AppLocalizationService>();
builder.Services.AddScoped<IDatabaseLocalizationService, DatabaseLocalizationService>();
builder.Services.AddScoped<ITranslationManagementService, TranslationManagementService>();
builder.Services.AddScoped<ILocalizationCenterService, LocalizationCenterService>();

// ── Localization ─────────────────────────────────────────────
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new System.Globalization.CultureInfo("ar-SA"), new System.Globalization.CultureInfo("en-US") };
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("ar-SA");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// ── Middleware Pipeline ──────────────────────────────────────
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection(); // فعّلها عند النشر على IIS مع شهادة SSL
app.UseStaticFiles();
app.UseRequestLocalization();
app.UseRouting();

// Security Headers
app.Use(async (context, next) =>
{
    context.Response.Headers["X-Frame-Options"]        = "SAMEORIGIN";
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["X-XSS-Protection"]       = "1; mode=block";
    context.Response.Headers["Referrer-Policy"]        = "strict-origin-when-cross-origin";
    await next();
});

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// ── Routes ───────────────────────────────────────────────────
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

// ── DB Init on startup ────────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
    await RuntimeSchema.EnsureV9SchemaAsync(db);
    await RuntimeSeed.SeedCloudAndVmwareAsync(db);
    await RuntimeSeed.SeedV9EnterpriseAsync(db);
    await RuntimeSeed.SeedV10DemoEnterpriseDataAsync(db);
    var dbLoc = scope.ServiceProvider.GetRequiredService<IDatabaseLocalizationService>();
    await dbLoc.SeedWave1KeysAsync();
}

app.Run();
