//our main hub to control the site
//imports and libraries

//core identity for login and user management
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

//our own components and data
using Try_Catch_Masters_Project.Components;
using Try_Catch_Masters_Project.Data;


/////////////////////////////////////////////////////////////////////////////////////////////
//Builder Section (what our app needs to run): Identity, Blazor, Razor Pages, database, etc.
/////////////////////////////////////////////////////////////////////////////////////////////
///Blazor
/// Database connection and context
/// Identity (user management, login, etc.)
/// Cookie settings for Identity


var builder = WebApplication.CreateBuilder(args);

//first we need to add blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//then we need to add our database connection and tell it where to find the connection string
//we put the connection string in appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//then we need to add our database context and tell it to use the connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Register Identity - links users to the database
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Tell Identity where to redirect when login is required
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
});

// Allows <AuthorizeView> to work in components (shows/hides nav links based on auth)
builder.Services.AddCascadingAuthenticationState();



/////////////////////////////////////////////////////////////////////
//App Section:
/////////////////////////////////////////////////////////////////////
/// HTTPs redirection
/// Authentication and Authorization 
/// Route to the right page

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}


app.UseHttpsRedirection();

// These two MUST be in this order, and BEFORE UseAntiforgery
app.UseAuthentication();  // "Who are you?"
app.UseAuthorization();   // "Are you allowed in?"

// This adds antiforgery tokens to all POST forms, and checks them on the server side
app.UseAntiforgery();

// Logout endpoint - signs the user out and redirects to /login
app.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Redirect("/login");
}).DisableAntiforgery();


app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
