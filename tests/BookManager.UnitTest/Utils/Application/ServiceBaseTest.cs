using BookManager.Domain.Interface.Common;
using BookManager.IoC.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace BookManager.UnitTest.Utils.Application
{
    public class ServiceBaseTest<TService, TEntity>
            where TService : class
            where TEntity : class
    {
        protected readonly ServiceProvider _provider;
        protected readonly TService _service;
        public ServiceBaseTest()
        {
            var services = new ServiceCollection();
            
            ConfigureDefaultServices(services);

            ConfigureServices(services);

            services.AddTransient<TService>();

            _provider = services.BuildServiceProvider();
            _service = _provider.GetRequiredService<TService>();
        }

        private static void ConfigureDefaultServices(ServiceCollection services)
        {
            services.AutoMapperConfig();
            services.ConfigFluentValidation();
            services.AddSingleton(new Mock<INotifier>().Object);
        }

        protected virtual void ConfigureServices(IServiceCollection services) { }
    }
}
