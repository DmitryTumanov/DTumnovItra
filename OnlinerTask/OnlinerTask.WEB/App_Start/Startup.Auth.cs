using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using OnlinerTask.WEB.Providers;
using OnlinerTask.WEB.Models;
using Microsoft.Owin.Security.Twitter;
using Microsoft.Owin.Security;
using OnlinerTask.Data.IdentityModels;

namespace OnlinerTask.WEB
{
    public partial class Startup
    {
        // Enable the application to use OAuthAuthorization. You can then secure your Web APIs
        static Startup()
        {
            PublicClientId = "web";

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                AuthorizeEndpointPath = new PathString("/Account/Authorize"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(20),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            app.UseTwitterAuthentication(
            new TwitterAuthenticationOptions
            {
                ConsumerKey = "OvdaXSkVSssliH7zKdmgURwWG",
                ConsumerSecret = "Dm6pUhHXyQGiPLZea6cTO9S0a0E9AeDXro0zNjNNudaGDWr8NF",
                BackchannelCertificateValidator = new CertificateSubjectKeyIdentifierValidator(
            new[]
            {
              "A5EF0B11CEC04103A34A659048B21CE0572D7D47",
               "0D445C165344C1827E1D20AB25F40163D8BE79A5",
               "7FD365A7C2DDECBBF03009F34339FA02AF333133",
               "39A55D933676616E73A761DFA16A7E59CDE66FAD",
               "4EB6D578499B1CCF5F581EAD56BE3D9B6744A5E5",
               "5168FF90AF0207753CCCD9656462A212B859723B",
               "B13EC36903F8BF4701D498261A0802EF63642BC3",
               "B77DDB6867D3B325E01C90793413E15BF0E44DF2"
            })
            });

            app.UseFacebookAuthentication(
               appId: "651925884980913",
               appSecret: "3f4a54efb64c5e4c1959df57e7d13a1a");

            app.UseVkontakteAuthentication(
                appId: "5663743",
                appSecret: "nfNHWv61xeYGbS9XAXMz",
                scope: "email,");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}
