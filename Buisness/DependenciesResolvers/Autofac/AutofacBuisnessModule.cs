using Autofac;
using Buisness.Abstract;
using Buisness.Concrete;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

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

       
    }
}