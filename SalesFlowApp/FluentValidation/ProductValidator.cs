using FluentValidation;
using SalesFlowApp.Models;


namespace SalesFlowApp.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El campo es requerido");
            RuleFor(x => x.IdCategoria).NotEmpty().WithMessage("El campo es requerido");
            RuleFor(x => x.Price).NotEmpty().WithMessage("El campo es requerido");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<Product>.CreateWithOptions((Product)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
