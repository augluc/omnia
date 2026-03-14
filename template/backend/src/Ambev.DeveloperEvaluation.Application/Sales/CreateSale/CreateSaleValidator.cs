using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Validador para garantir que os dados do comando estão preenchidos corretamente.
    /// </summary>
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(x => x.SaleNumber).NotEmpty().WithMessage("SaleNumber is required.");
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.CustomerName).NotEmpty();
            RuleFor(x => x.BranchId).NotEmpty();
            RuleFor(x => x.BranchName).NotEmpty();

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("A sale must have at least one item.");

            RuleForEach(x => x.Items).SetValidator(new CreateSaleItemValidator());
        }
    }

    public class CreateSaleItemValidator : AbstractValidator<CreateSaleItemCommand>
    {
        public CreateSaleItemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty();

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
                .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");

            RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("UnitPrice must be greater than zero.");
        }
    }
}
