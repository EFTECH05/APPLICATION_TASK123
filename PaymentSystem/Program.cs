using Microsoft.EntityFrameworkCore;
using PaymentSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// ================= SERVICES =================
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

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

builder.Services.AddAuthorization();

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

// 🔐 SESSION MUST BE BEFORE SECURITY CHECKS
app.UseSession();

app.UseAuthorization();


// ================= 🔥 GLOBAL ADMIN BLOCK (IMPORTANT FIX) =================
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value;

    // Block ALL admin access if not authenticated
    if (path != null && path.StartsWith("/Admin"))
    {
        var user = context.Session.GetString("User");
        var role = context.Session.GetString("Role");

        if (string.IsNullOrEmpty(user) ||
            (role != "Admin" && role != "SuperAdmin"))
        {
            context.Response.StatusCode = 404; // hide admin completely
            return;
        }
    }

    await next();
});

// ================= ROUTES =================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// ================= SEED SUPER ADMIN =================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Users.Any(u => u.Role == "SuperAdmin"))
    {
        db.Users.Add(new User
        {
            Name = "Main Admin",
            Email = "admin@globaltrust.com",
            Password = "123456",
            Role = "SuperAdmin",
            Status = "Approved",
            IsApproved = true,
            CreatedBy = "System",
            CreatedAt = DateTime.Now
        });

        db.SaveChanges();
    }
}

app.Run();