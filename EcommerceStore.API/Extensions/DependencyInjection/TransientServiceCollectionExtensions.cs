using EcommerceStore.Application.Interfaces;
using EcommerceStore.Application.Services;
using EcommerceStore.Domain.Interfaces;
using EcommerceStore.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceStore.API.Extensions.DependencyInjection
{
    public static class TransientServiceCollectionExtensions
    {
        public static IServiceCollection AddTransientDomainServices(this IServiceCollection services)
        {
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IOrderService, OrderService>();

            return services;
        }

        public static IServiceCollection AddTransientRepositories(this IServiceCollection services)
        {
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}