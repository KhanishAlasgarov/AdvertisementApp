


using AdvertisementApp.DataAccess.Contexts;
using AdvertisementApp.DataAccess.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdvertisementApp.Application.DependencyResolvers.Microsoft;

public static class DependencyExtension
{
    public static void AddDependencies(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<AdvertisementAppContext>((opt) =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("Local"));
        });

        var mapperConfiguration = new MapperConfiguration(opt =>
        {

        });
        var mapper = mapperConfiguration.CreateMapper();


        serviceCollection.AddSingleton(mapper);

        serviceCollection.AddScoped<IUow, Uow>();
    }
}
