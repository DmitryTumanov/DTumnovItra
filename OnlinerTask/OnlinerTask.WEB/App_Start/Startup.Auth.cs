using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using OnlinerTask.WEB.Providers;
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
        
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(20),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
            
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            
            app.UseOAuthBearerTokens(OAuthOptions);
            

            app.UseTwitterAuthentication(
            new TwitterAuthenticationOptions
            {
                ConsumerKey = "78Rm3ihN9l05bqwoc8pPgJLfp",
                ConsumerSecret = "CYjyJBNDXc9gzeWTuB7IvG8HFWPoJiOJzEyKpVu3geYDWfJ8BS",
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
               appId: "1037987856324156",
               appSecret: "c20d538ac708f6029c2758afcf91d4d8");

            app.UseVkontakteAuthentication(
                appId: "5791429",
                appSecret: "dDzPTaB2U6WuusA4N20T",
                scope: "email,");
        }
    }
}
