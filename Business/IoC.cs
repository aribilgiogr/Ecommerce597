using Core.Concretes.Entities;
using Core.Utils.GenericRepositoryPattern;
using Data;
using Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business
{
    public static class IoC
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShopContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("shop")));

            services.AddIdentity<Member, IdentityRole>()
                    .AddEntityFrameworkStores<ShopContext>()
                    .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }
}
