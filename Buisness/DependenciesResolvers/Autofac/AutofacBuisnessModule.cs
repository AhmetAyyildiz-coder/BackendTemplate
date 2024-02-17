using Autofac;
using Autofac.Extras.DynamicProxy;
using Buisness.Abstract;
using Buisness.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Core.Utilities.Interceptors;

namespace Buisness.DependenciesResolvers.Autofac;

/// <summary>
/// IOC container 
/// </summary>
public class AutofacBuisnessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ProductManager>().As<IProductService>();
        builder.RegisterType<EfProductDal>().As<IProductDal>();


        builder.RegisterType<CategoryManager>().As < ICategoryService>();
        builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

        builder.RegisterType<UserManager>().As<IUserService>();
        builder.RegisterType<EfUserDal>().As<IUserDal>();

        builder.RegisterType<AuthManager>().As<IAuthService>();

        builder.RegisterType<JwtHelper>().As<ITokenHelper>();


        builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

        builder.RegisterType<OperationClaimDal>().As<IOperationClaimDal>();



        // dynamic proxy olusturabilmek icin gerekli ayarlamalar
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();


        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(
                new ProxyGenerationOptions()
                {
                    // selector ile araya girecek nesneyi belirlemeliyiz.
                    Selector =new AspectInterceptorSelector()
                }
                ).SingleInstance();
    }
}