﻿namespace Shop.RealEstateTest;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.ApplicationServices.SpaceshipServices;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.RealEstateTest.Macros;
using Shop.RealEstateTest.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class TestBase
{
    protected IServiceProvider ServiceProvider { get; set; }

    protected TestBase()
    {
        var services = new ServiceCollection();
        SetupServices(services);
        ServiceProvider = services.BuildServiceProvider();
    }

    protected T Svc<T>()
    {
        return ServiceProvider.GetService<T>();
    }

    public virtual void SetupServices(IServiceCollection services)
    {
        services.AddScoped<IRealEstateService, RealEstateService>();
        services.AddScoped<IKindergartenService, KindergartenService>();
        services.AddScoped<IFileService, FileServices>();
        services.AddScoped<IHostEnvironment, MockHostEnvironment>();

        services.AddDbContext<ShopContext>(x =>
        {
            x.UseInMemoryDatabase("TEST");
            x.ConfigureWarnings(e => e.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        });

        RegisterMacros(services);
    }

    private void RegisterMacros(IServiceCollection services)
    {
        var macroBaseType = typeof(IMacros);
        var macros = macroBaseType.Assembly.GetTypes()
            .Where(x => macroBaseType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

        foreach (var macro in macros)
        {
            services.AddSingleton(macro);
        }
    }
}
