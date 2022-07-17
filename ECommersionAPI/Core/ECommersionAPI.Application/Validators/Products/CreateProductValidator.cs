using ECommersionAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ECommersionAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Products>
    {
        public CreateProductValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotEmpty()
                    .WithMessage("Name field is null!")
                .MaximumLength(150)
                .MinimumLength(5)
                    .WithMessage("Name leng 5 to 150 ");

            RuleFor(c => c.Stock)
                .NotEmpty()
                .NotEmpty()
                    .WithMessage("Stock field is null!")
                .Must(s => s > 0)
                    .WithMessage("Stock field is not under 0");

            RuleFor(c => c.Price)
               .NotEmpty()
               .NotEmpty()
                   .WithMessage("Price field is null!")
               .Must(s => s > 0)
                   .WithMessage("Price field is not under 0");


        }
    }
}
