using Core.Utilities.Messages;
using FluentValidation;


namespace Core.CrossCuttingConcerns.Validation;

public class ValidationTool
{
    //public static void Validate<T>(IValidator validator, object entity)
    //{
    //    var result = validator.Validate(entity);
    //    if (!result.IsValid)
    //    {
    //        throw new ArgumentException(AspectMessages.WrongValidationType);
    //    }
    //}

    /// <summary>
    /// Gelen validator nesnesi ile gelen entity'yi birbiriyle ilişkilendirip
    /// AbstractValidator sınıfından kalıtım alan validator sınıfını bulur. Bu sınıfın
    /// validate metodunu kullanır. 
    /// </summary>
    /// <param name="TValidator"></param>
    /// <param name="entity"></param>
    /// <exception cref="ArgumentException"></exception>
    public static void Validate(IValidator TValidator, object entity)
    {

        var context = new ValidationContext<object>(entity);
        var result = TValidator.Validate(context);
        if (!result.IsValid)
        {
            throw new ArgumentException(AspectMessages.WrongValidationType);
        }
    }
}