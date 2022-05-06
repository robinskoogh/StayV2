using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Data.Data;
using Core.Helpers;
using Core.Models;

//var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StayContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StayContext")));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false) // Set to true to enable email verification.
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<StayContext>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = "993481163782-q195aarhnhu42ennqb4av5v9tkkk64vo.apps.googleusercontent.com";    // Should be put into secrets on deployment
    googleOptions.ClientSecret = "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";                                     // Same as above
});


builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();