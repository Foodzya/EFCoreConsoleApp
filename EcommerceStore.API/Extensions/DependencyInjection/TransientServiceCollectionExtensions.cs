using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Services;
using EcommerceStore.Domain.Interfaces;
using EcommerceStore.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceStore.API.Extensions.DependencyInjection
{
    public static class TransientServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<ISectionService, SectionService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IAccountService, AccountService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<ISectionRepository, SectionRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();

            return services;
        }
    }
}