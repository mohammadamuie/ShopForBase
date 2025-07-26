using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Contracts.Infrastructure;
using Project.Application.Models;
using Project.Infrastructure.FileStorage;
using Project.Infrastructure.Mail;
using Project.Infrastructure.Sms;
using RestSharp;

namespace Project.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            services.Configure<SmsSettings>(configuration.GetSection("SmsSettings"));
            services.AddTransient<ISmsSender, SmsSender>();

            services.Configure<ArvanCloudSettings>(configuration.GetSection("ArvanCloudSettings"));
            //services.AddScoped<IFileStorageService, ArvanCloudStorageService>();
            services.AddScoped<IFileStorageService, InAppStorageService>();

            //services.AddSingleton<ILoyaltycCutService>(new LoyaltyCutService(new RestClient()));

            return services;
        }
    }
}
