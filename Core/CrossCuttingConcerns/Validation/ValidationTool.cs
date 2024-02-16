using Core.Utilities.Messages;
using FluentValidation;
using FluentValidation.Results;

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