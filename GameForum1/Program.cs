

using GameForum1.DAL;
using GameForum1.Models;

namespace GameForum1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("GameForum1ContextConnection") ?? throw new InvalidOperationException("Connection string 'GameForum1ContextConnection' not found.");

        //builder.Services.AddScoped<UserThread>();
        //builder.Services.AddScoped<List<UserThread>>();

        builder.Services.AddTransient<ProfilePictureManager>();
        builder.Services.AddTransient<GameForum1Context>();
        builder.Services.AddTransient<MainCategory>();
        builder.Services.AddTransient<List<MainCategory>>();
        builder.Services.AddTransient<List<SubCategory>>();
        builder.Services.AddTransient<List<UserThread>>();
        builder.Services.AddTransient<List<Comment>>();
        builder.Services.AddTransient<SubCategory>();
        builder.Services.AddTransient<MessageManager>();

        builder.Services.AddRazorPages(options =>
        {
            options.Conventions.AuthorizeFolder("/Admin");
            options.Conventions.AuthorizeFolder("/Admin", "AdminRequired");
        });
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminRequired", policy => policy.RequireRole("Admin"));
        });

        builder.Services.AddDbContext<GameForum1Context>(options => options.UseSqlServer(connectionString));

        builder.Services.AddDefaultIdentity<GameForum1User>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<GameForum1Context>();

        // Add services to the container.
        //builder.Services.AddRazorPages();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Default User settings. // man kunde ha @ och +, samt unique email = false
            options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzåäöABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ0123456789-._";
            options.User.RequireUniqueEmail = true;
            // Default Password settings.  == true,true,true,true
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
        });


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}