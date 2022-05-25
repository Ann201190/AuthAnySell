using Auth.Business.Context;
using Auth.Business.Service;
using Auth.Business.Service.Interfaces;
using Auth.Business.SmtpClientEmailSender;
using Auth.Business.SmtpClientEmailSender.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Business
{
    public static class Startup
    {
        public static void Start(IServiceCollection services, string connectStr)
        {
            //Dependency injection:
            services.AddTransient<IAuthService, AuthService>(); 
            services.AddTransient<IJWTService, JWTService>();
            services.AddTransient<IRegistrationService, RegistrationService>();
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddDbContext<CustomDbContext>(options => options.UseSqlServer(connectStr));
        }
    }
}
