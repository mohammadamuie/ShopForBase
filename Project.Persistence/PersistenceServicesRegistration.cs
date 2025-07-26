using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Application.Contracts.Persistence;
using Project.Application.Features.Interfaces;
using Project.Application.Features.Services;
using Project.Persistence.Repositories;

namespace Project.Persistence
{

    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                //options.UseLazyLoadingProxies();
                options.EnableSensitiveDataLogging(true);
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.UseNetTopologySuite());
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserInformationRepository, UserInformationRepository>();
            services.AddScoped<IIdentityUserRepository, IdentityUserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductImagesRepository, ProductImagesRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IDiscountCodeRepository, DiscountCodeRepository>();
            services.AddScoped<IPagesRepository, PagesRepository>();
            services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<INewsCategoryRepository, NewsCategoryRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketMessageRepository, TicketMessageRepository>(); 
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IPurchaseRequestRepository, PurchaseRequestRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IFactorRepository, FactorRepository>();
            services.AddScoped<IBannerRepository, BannerRepository>();

            return services;
        }
    }
}