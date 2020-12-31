using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessWorker.Bl.Interfaces;

namespace ProcessWorker.Bl
{
    public static class Util
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IAuthorization, Authorization>();
            services.AddTransient<IProcessManager, ProcessManager>();
            services.Configure<QueueOptions>(config.GetSection("QueueConfig"));
            services.AddSingleton<IMessageBroker, MessageBroker>();
            return services;
        }
    }
}