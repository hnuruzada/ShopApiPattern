using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shop.Api.Extensions;

namespace Shop.Api.Apps.AdminApi.DTOs.ProductDtos
{
    public class ProductPostDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public IFormFile Image { get; set; }
    }
    public class ProductPostDtoValidator : AbstractValidator<ProductPostDto>
    {
        public ProductPostDtoValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(x => x.Name)
                .MaximumLength(50).WithMessage("Uzunluq max 50 ola biler , qaqa!")
                .NotEmpty().WithMessage("Name mecburidir!");

            RuleFor(x => x.CostPrice)
                .GreaterThanOrEqualTo(0).WithMessage("CostPrice 0-dan asagi ola bilmez!")
                .NotNull().WithMessage("CostPrice mecburidir!");

            RuleFor(x => x.SalePrice)
                .GreaterThanOrEqualTo(0).WithMessage("SalePrice 0-dan asagi ola bilmez!")
                .NotNull().WithMessage("SalePrice mecburidir!");
            RuleFor(x => x.Image).Custom((x, content) =>
            {
                if (!x.IsImage())
                {
                    content.AddFailure("Image", "Choose correct image file");
                }
                if (!x.IsSizeOkay(2))
                {
                    content.AddFailure("Image", "File size must be max 2MB");
                }
            }).NotEmpty().WithMessage("File is null choose image file");

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.CostPrice > x.SalePrice)
                    context.AddFailure("CostPrice", "CostPrice SalePrice-dan boyuk ola bilmez");
            });
        }
    }
}