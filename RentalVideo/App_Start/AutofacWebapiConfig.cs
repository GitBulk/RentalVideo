using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using RentalVideo.Data;
using RentalVideo.Data.Infrastructure;
using RentalVideo.Services;
using RentalVideo.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace RentalVideo.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            // http://docs.autofac.org/en/latest/advanced/aggregate-services.html
            // http://stackoverflow.com/questions/15600440/how-to-use-property-injection-with-autofac/23535219#23535219
            //http://bling.github.io/blog/2009/09/07/member-injection-module-for-autofac/
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterAggregateService<IMyAggregateService>();
            builder.RegisterType<RentalVideoContext>().As<DbContext>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterGeneric(typeof(EntityBaseRepository<>)).As(typeof(IEntityBaseRepository<>)).InstancePerRequest();
            builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerRequest();
            builder.RegisterType<MembershipService>().As<IMembershipService>().InstancePerRequest();
            Container = builder.Build();
            return Container;
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //// EF HomeCinemaContext
            //builder.RegisterType<RentalVideoContext>().As<DbContext>().InstancePerRequest();
            //builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            //builder.RegisterType<UnitOfWork>()
            //    .As<IUnitOfWork>()
            //    .InstancePerRequest();

            //builder.RegisterGeneric(typeof(EntityBaseRepository<>))
            //       .As(typeof(IEntityBaseRepository<>))
            //       .InstancePerRequest();

            //// Services
            //builder.RegisterType<EncryptionService>()
            //    .As<IEncryptionService>()
            //    .InstancePerRequest();

            //builder.RegisterType<MembershipService>()
            //    .As<IMembershipService>()
            //    .InstancePerRequest();

            //Container = builder.Build();

            //return Container;
        }
    }
}