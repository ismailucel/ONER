using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace SuggestionAppUI;

public static class RegisterServices
{
   // Extension method for extending the dependency injection services
   public static void ConfigureServices(this WebApplicationBuilder builder)
   {

      // Add services to the container.
      builder.Services.AddRazorPages();
      builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();
      builder.Services.AddMemoryCache();
      builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();

      builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));

      builder.Services.AddAuthorization(options =>
      {
         options.AddPolicy("Admin", policy =>
         {
            policy.RequireClaim("jobTitle", "Admin");
         });
      });

      // Add Dependency Injection container for DataAccess classes
      builder.Services.AddSingleton<IDbConnection, DbConnection>(); // creates a single instance for everyone
      builder.Services.AddSingleton<ICategoryData, MongoCategoryData>();
      builder.Services.AddSingleton<IStatusData, MongoStatusData>();
      builder.Services.AddSingleton<IUserData, MongoUserData>();
      builder.Services.AddSingleton<ISuggestionData, MongoSuggestionData>();
   }
}
