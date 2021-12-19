using System.Collections.Generic;
using System.Reflection;
using Autofac;
using AutoMapper;
using HepsiBuradaAssignment.Application.AutoMapper;
using HepsiBuradaAssignment.Application.Commands;
using HepsiBuradaAssignment.Application.Queries;
using HepsiBuradaAssignment.Domain.Interfaces;
using HepsiBuradaAssignment.Infrastructure.Repositories;
using MediatR;

namespace HepsiBuradaAssignment.Api.App.AutoFacModule
{
    public class ApplicationModule:Autofac.Module
    {
        public ApplicationModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();


            builder.RegisterAssemblyTypes(typeof(CreateProductCommand).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(CreateCampaignCommand).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(CreateOrderCommand).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterType<ProductQueries>().As<IProductQueries>().InstancePerLifetimeScope();
            builder.RegisterType<CampaignQueries>().As<ICampaignQueries>().InstancePerLifetimeScope();

            builder.RegisterType<ProductRepository>().As<IProductRepository>().SingleInstance();
            builder.RegisterType<CampaignRepository>().As<ICampaignRepository>().SingleInstance();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().SingleInstance();

            builder.RegisterAssemblyTypes().AssignableTo(typeof(AutoMapperProfile));

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());

            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}
