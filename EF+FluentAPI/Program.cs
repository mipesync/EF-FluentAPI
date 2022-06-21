using EF_FluentAPI;
using EF_FluentAPI.BLL;
using EF_FluentAPI.BLL.Implementations;
using EF_FluentAPI.BLL.Interfaces;
using EF_FluentAPI.DbContexts;
using EF_FluentAPI.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("SQLite");

builder.Services.AddDbContext<DBContext>(options => options.UseSqlite(connection));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthOptions.ISSUER,
        ValidateAudience = true,
        ValidAudience = AuthOptions.AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();

builder.Services.AddControllers(options => options.Filters.Add(typeof(Logger)));
builder.Services.AddTransient<ConsoleLogger>();
builder.Services.AddTransient<FileLogger>();
builder.Services.AddTransient<CombinedLogger>();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<ICredentialService, CredentialService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddScoped<ServiceManager>();

builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy => policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("https://localhost:7144"));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();