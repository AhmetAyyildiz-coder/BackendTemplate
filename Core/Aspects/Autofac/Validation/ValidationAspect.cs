using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation;

public class ValidationAspect: MethodInterception
{
    public Type _validatiorType;
    public ValidationAspect(Type validatorType, int priority)
    {
        



        this.Priority = priority;

        // parametre gelen validation type IValidator'dan türetilmemişse error ver 
        if (!typeof(IValidator).IsAssignableFrom(validatorType))
        {
            throw new ArgumentException("Wrong");
        }

        _validatiorType = validatorType;
    }

    protected override void OnBefore(IInvocation invocation)
    {
        var validator = (IValidator)Activator.CreateInstance(_validatiorType)!;

        /**
         * public class ProductValidator : AbstractValidator<Product> base type = Abstract Valiadot olur.
         * Generic argumants ise <product> olarak bulur </product>
         */
        var entityType = _validatiorType.BaseType.GetGenericArguments()[0];

        var entities = invocation.Arguments.Where(t => t.GetType() == entityType);

        foreach (var entity in entities)
        {
            ValidationTool.Validate(validator,entity);
        }

    }
}