using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors;

/// <summary>
/// Bir metodu nasıl yorumlayacağımızı belirleyen sınıftır
/// Aslında bir method'un nasıl ele alınacağını yazmış olduk. Süreci baştan sona takip edebiliyoruz.
/// </summary>
public abstract class MethodInterception : MethodInterceptionBaseAttribute
{
    //bu kodu nerde nasıl yorumlayacağımızı anlatır. 


    /// <summary>
    /// Method Çalışmadan önce sen çalış demek istiyoruz.
    /// İnvocation => çalıştırılmak istenilen metodu temsil eder. 
    /// </summary>
    protected virtual void OnBefore(IInvocation invocation){}
    protected virtual void OnAfter(IInvocation invocation) { }
    protected virtual void OnException(IInvocation invocation) { }
    protected virtual void OnSuccess(IInvocation invocation) { }

    public override void Intercept(IInvocation invocation)
    {
        var isSuccess = true;

        OnBefore(invocation);

        try
        {
            // proceed ile metodu çalıştır diyoruz.
            invocation.Proceed();
        }
        catch (Exception e)
        {
            isSuccess = false;
            OnException(invocation);
        }
        finally
        {
            if (isSuccess)
            {
                // eğer method başarılı olursa success çalışsın
                OnSuccess(invocation);
            }
        }

        OnAfter(invocation);
    }
}