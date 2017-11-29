namespace Employees.Client
{
    using System;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Employees.Data;
    using Employees.Service;
    using Employees.Client.Core;

    class StartUp
    {
        static void Main()
        {

            var serviceProvider = ConfigureServices();
            Mapper.Initialize(cfg => cfg.AddProfile<AutomapperProfile>());
            var engine = new Engine(serviceProvider);
            engine.Run();
        }

        static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EmployeesContext>(options => options.UseSqlServer(ServerConfig.connectionString));

            serviceCollection.AddTransient<EmployeeService>();

            //serviceCollection.AddAutoMapper()

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;

        }
    }
}
