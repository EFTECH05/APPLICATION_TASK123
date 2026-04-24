using Microsoft.EntityFrameworkCore;
using PaymentSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// ================= SERVICES =================
builder.Services.AddControllersWithViews();

// ================= DATABASE =================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// ================= SESSION =================
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ================= AUTHORIZATION (IMPORTANT) =================
builder.Services.AddAuthorization();

// Needed for session access in controllers/views
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// ================= PIPELINE =================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🔥 MUST BE IN THIS ORDER
app.UseSession();

app.UseAuthorization();

// ================= ROUTES =================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// ================= SEED SUPER ADMIN (VERY IMPORTANT) =================
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Users.Any(u => u.Role == "SuperAdmin"))
    {
        context.Users.Add(new User
        {
            Name = "Main Admin",
            Email = "admin@globaltrust.com",
            Password = "123456",
            Role = "SuperAdmin",
            Status = "Approved",
            IsApproved = true,
            CreatedBy = "System"
        });

        context.SaveChanges();
    }
}

app.Run();