using Entities.Concrete;
using FluentValidation;

namespace Buisness.ValidationRules.FluentValidation;

public class ProductValidator : AbstractValidator<Product>
{
    // NOT : Validation'larda buisness kod yazılmaz !!!!
    public ProductValidator()
    {
        RuleFor(p => p.ProductName).NotEmpty();
        RuleFor(p => p.ProductName).Length(5, 20)
            .WithMessage("Ürün adı uzunluğu 5 karakter ile 20 karakter arasında olmalıdır.");
        RuleFor(p => p.UnitPrice).NotEmpty();
        // unit price 1'den büyük olmalı - ne zaman -> category Id = 1 olanlar icin gecerli bu.
        RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1).When(p => p.CategoryId == 1);
    }
}