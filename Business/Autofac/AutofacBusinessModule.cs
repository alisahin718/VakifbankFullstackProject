using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Auth;
using Business.Repositories.Manager;
using Business.Repositories.Service;
using Core.Utilities.Interceptors;
using Core.Utilities.Jwt;
using DataAccess.Repositories.Contract;
using DataAccess.Repositories.EFCore;
using Module = Autofac.Module;

namespace Business.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<OperationClaimRepository>().As<IOperationClaimRepository>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();

            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<UserOperationClaimRepository>().As<IUserOperationClaimRepository>();

            //builder.RegisterType<EmailParameterManager>().As<IEmailParameterService>();
            //builder.RegisterType<EfEmailParameterDal>().As<IEmailParameterDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();

            builder.RegisterType<TokenHandler>().As<ITokenHandler>();

            builder.RegisterType<BasketManager>().As<IBasketService>().SingleInstance();
            builder.RegisterType<BasketRepository>().As<IBasketRepository>().SingleInstance();

            builder.RegisterType<DealerRelationshipManager>().As<IDealerRelationshipService>().SingleInstance();
            builder.RegisterType<DealerRelationshipRepository>().As<IDealerRelationshipRepository>().SingleInstance();

            builder.RegisterType<DealerManager>().As<IDealerService>().SingleInstance();
            builder.RegisterType<DealerRepository>().As<IDealerRepository>().SingleInstance();

            builder.RegisterType<OrderDetailManager>().As<IOrderDetailService>().SingleInstance();
            builder.RegisterType<OrderDetailRepository>().As<IOrderDetailRepository>().SingleInstance();

            builder.RegisterType<OrderManager>().As<IOrderService>().SingleInstance();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().SingleInstance();

            builder.RegisterType<PriceListDetailManager>().As<IPriceListDetailService>().SingleInstance();
            builder.RegisterType<PriceListDetailRepository>().As<IPriceListDetailRepository>().SingleInstance();

            builder.RegisterType<PriceListManager>().As<IPriceListService>().SingleInstance();
            builder.RegisterType<PriceListRepository>().As<IPriceListRepository>().SingleInstance();

            builder.RegisterType<ProductImageManager>().As<IProductImageService>().SingleInstance();
            builder.RegisterType<ProductImageRepository>().As<IProductImageRepository>().SingleInstance();

            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<ProductRepository>().As<IProductRepository>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
        }
    }
}
