namespace Shop.RealEstateTest;

using Microsoft.Extensions.DependencyInjection;
using Shop.Core.ServiceInterface;
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

    }
}
