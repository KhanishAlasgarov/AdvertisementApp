


using AdvertisementApp.Application.Interfaces;
using AdvertisementApp.Application.Mappings.AutoMapper;
using AdvertisementApp.Application.Services;
using AdvertisementApp.Application.ValidationRules.AdvertisementDtoValidators;
using AdvertisementApp.Application.ValidationRules.AppUserDtoValidators;
using AdvertisementApp.Application.ValidationRules.GenderDtoValidators;
using AdvertisementApp.Application.ValidationRules.ProvidedServiceDtoValidators;
using AdvertisementApp.DataAccess.Contexts;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Dtos;
using AutoMapper;
using FluentValidation;
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
            opt.AddProfile(new ProvidedServiceProfile());
            opt.AddProfile(new AdvertisementProfile());
            opt.AddProfile(new AppUserProfile());
            opt.AddProfile(new GenderProfile());
        });
        var mapper = mapperConfiguration.CreateMapper();


        serviceCollection.AddSingleton(mapper);

        serviceCollection.AddScoped<IUow, Uow>();

        serviceCollection.AddTransient<IValidator<ProvidedServiceCreateDto>, ProvidedServiceCreateDtoValidator>();
        serviceCollection.AddTransient<IValidator<ProvidedServiceUpdateDto>, ProvidedServiceUpdateDtoValidator>();

        serviceCollection.AddTransient<IValidator<AdvertisementUpdateDto>, AdvertisementUpdateDtoValidator>();
        serviceCollection.AddTransient<IValidator<AdvertisementCreateDto>, AdvertisementCreateDtoValidator>();

        serviceCollection.AddTransient<IValidator<AppUserCreateDto>, AppUserCreateDtoValidator>();
        serviceCollection.AddTransient<IValidator<AppUserUpdateDto>, AppUserUpdateDtoValidator>();

        serviceCollection.AddTransient<IValidator<GenderCreateDto>, GenderCreateDtoValidator>();
        serviceCollection.AddTransient<IValidator<GenderUpdateDto>, GenderUpdateDtoValidator>();
        serviceCollection.AddTransient<IValidator<AppUserLoginDto>, AppUserLoginDtoValidator>();

        serviceCollection.AddScoped<IProvidedServiceService, ProvidedServiceService>();
        serviceCollection.AddScoped<IAdvertisementService, AdvertisementService>();
        serviceCollection.AddScoped<IAppUserService, AppUserService>();
        serviceCollection.AddScoped<IGenderService, GenderService>();
    }
}
