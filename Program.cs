//our main hub to control the site
//imports and libraries

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Try_Catch_Masters_Project.Components;
using Try_Catch_Masters_Project.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔥 FIX PARA RENDER (PUERTO)
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

/////////////////////////////////////////////////////////////////////////////////////////////
// SERVICES
/////////////////////////////////////////////////////////////////////////////////////////////

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
});

builder.Services.AddCascadingAuthenticationState();

/////////////////////////////////////////////////////////////////////////////////////////////
// APP
/////////////////////////////////////////////////////////////////////////////////////////////

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// ⚠️ IMPORTANTE: SOLO HTTP EN RENDER
// ❌ app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Redirect("/login");
}).DisableAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();