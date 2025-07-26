using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Project.Application.Profiles;
using NetTopologySuite.Geometries;
using NetTopologySuite;
using Project.Application.Features.Services;
using Project.Application.Features.Interfaces;
using Parbad.Builder;
using Parbad.Gateway.ZarinPal;

namespace Project.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton(provider => new MapperConfiguration(config =>
            {
                var geometryFactory = provider.GetRequiredService<GeometryFactory>();
                config.AddProfile(new MappingProfile(geometryFactory));
            }).CreateMapper());

            services.AddSingleton<GeometryFactory>(NtsGeometryServices
                .Instance.CreateGeometryFactory(srid: 4326));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IIdentityUserService, IdentityUserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IFactorService, FactorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IDiscountCodeService, DiscountCodeService>();
            services.AddScoped<IPagesService, PagesService>();
            services.AddScoped<IMenuItemService, MenuItemService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<INewsCategoryService, NewsCategoryService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IPurchaseRequestService, PurchaseRequestService>();
            services.AddScoped<IBannerService, BannerService>();

            services
               .AddParbad()
               .ConfigureGateways(gateways =>
               {
                   gateways
                       .AddZarinPal()
                       .WithAccounts(accounts =>
                       {
                           accounts.AddInMemory(account =>
                           {
                               account.IsSandbox = true;
                               //  account.LoginAccount = "1QQAuWhw2sB10G815V53";
                               account.MerchantId = "07161066-9834-11e9-ba5e-000c29344814";
                           });
                       });
               })
               .ConfigureHttpContext(builder => builder.UseDefaultAspNetCore())
               .ConfigureStorage(builder => builder.UseMemoryCache());


            return services;
        }
    }
}
