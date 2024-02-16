using System.Transactions;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;

namespace Core.Aspects.Autofac.Transaction;

public class TransactionScopeAspect : MethodInterception
{
    public override void Intercept(IInvocation invocation)
    {
        using TransactionScope scope = new();
        try
        {
            invocation.Proceed();
        }
        catch (Exception e)
        {
            scope.Dispose();
            throw;
        }
    }
}