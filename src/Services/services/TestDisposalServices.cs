using System;
using Microsoft.Extensions.DependencyInjection;

public class Service1 : IDisposable
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
public class Service2 : IDisposable
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}

public interface IService3 {}
public class Service3 : IService3, IDisposable
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
public class TestDisposeServices
{
    public void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<Service1>();
    services.AddSingleton<Service2>();
    services.AddSingleton<IService3>(sp => new Service3()); //Dispose automatically

    // does are not dispose automatically since the use create the object
    services.AddSingleton<Service1>(new Service1());
    services.AddSingleton(new Service2());
}
}
