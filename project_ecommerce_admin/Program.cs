using Microsoft.EntityFrameworkCore;
using project_ecommerce_admin.Data;
using project_ecommerce_admin.Repositories.Interface;
using project_ecommerce_admin.Repositories.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Repositories
builder.Services.AddScoped<IBrand, BrandService>();
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<IAddressShop, AddressShopService>();
builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<IProductImage, ProductImageService>();
builder.Services.AddScoped<IProductColor, ProductColorService>();
builder.Services.AddScoped<IProductOption, ProductOptionService>();
builder.Services.AddScoped<IProductAddressShop, ProductAddressShopService>();
builder.Services.AddScoped<IProductProperty, ProductPropertyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
