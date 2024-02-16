using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors;


[AttributeUsage(AttributeTargets.Class |AttributeTargets.Method
    ,AllowMultiple = true 
    ,Inherited = true)]
public class MethodInterceptionBaseAttribute : Attribute, IInterceptor
{
    // attribute çalışma sırası 
    public int Priority { get; set; }
    public virtual void Intercept(IInvocation invocation)
    {
        
    }
}