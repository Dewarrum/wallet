using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace Wallet.Api;

public static class WebApplicationBuilderExtensions
{
    public static void ConfigureAuthentication(this WebApplicationBuilder app)
    {
        app.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = app.Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = app.Configuration["Authentication:Google:ClientSecret"];
            });
    }

    public static void ConfigureAuthorization(this WebApplicationBuilder app)
    {
        app.Services.AddAuthorization();
    }

    public static void ConfigureSessions(this WebApplicationBuilder app)
    {
        app.Services.AddSession(options =>
        {
            options.Cookie.IsEssential = true;
        });
    }
}
