using Buisness.Constant;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.Ioc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Buisness.BuisnessAspect.Autofac;

public class SecuredOperation : MethodInterception
{
    private string[] _roles;
    private IHttpContextAccessor _accessor;

    public SecuredOperation(string roles)
    {// kullanıcının rollerini virgül ile ayırıyoruz. 
        _roles = roles.Split(',').ToArray();
        _accessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
    }

    protected override void OnBefore(IInvocation invocation)
    {
        var roleClaims = _accessor.HttpContext.User.ClaimRoles();

        foreach (var role in roleClaims)
        {
            if (roleClaims.Contains(role))
            {// gelen role parametresi kullanıcının rolüyle eşdeğer ise sorun yok.
                return;
            }
        }

        throw new ArgumentException(Messages.AccessDenied);
    }
}