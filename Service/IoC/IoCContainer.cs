using Autofac;
using Business;
using Common.Enum;
using Interface.Business;
using Interface.Repository;
using Interface.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using Model.Context;
using Repository;
using Service.HandleService;
using System;
using System.Collections.Generic;

namespace Service.IoC
{
    public sealed class IoCContainer
    {
        public static IContainer AutofacBuilder;
        public static void Initialize(IConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<LogInfo>().Keyed<IMessagingServiceProvider<SMS>>(MessageServiceType.LOG_INFO);
            builder.RegisterType<ProcessMessage>().Keyed<IMessagingServiceProvider<SMS>>(MessageServiceType.PROCESS_MESSAGE);

            builder.RegisterType<SMSBusiness>().As<ISMSBusiness>();
            builder.RegisterType<SMSRepository>().As<ISMSRepository>();
            builder.RegisterType<MittoContext>();

            RegisterContext<MittoContext>(builder, configuration);

            AutofacBuilder = builder.Build();
        }
        private static void RegisterContext<TContext>(ContainerBuilder builder, IConfiguration configuration) where TContext : DbContext
        {
            builder.Register(componentContext =>
            {
                //var serviceProvider = componentContext.Resolve<IServiceProvider>();
                //var configuration = componentContext.Resolve<IConfiguration>();
                var dbContextOptions = new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>());
                var optionsBuilder = new DbContextOptionsBuilder<TContext>(dbContextOptions)
                    //.UseApplicationServiceProvider(serviceProvider)
                    .UseSqlServer(configuration["ConnectionString"]);

                return optionsBuilder.Options;
            }).As<DbContextOptions<TContext>>()
                .InstancePerLifetimeScope();

            builder.Register(context => context.Resolve<DbContextOptions<TContext>>())
                .As<DbContextOptions>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TContext>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
