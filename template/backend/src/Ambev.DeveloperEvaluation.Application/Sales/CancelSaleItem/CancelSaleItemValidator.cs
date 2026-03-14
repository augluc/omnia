using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem
{
    public class CancelSaleItemValidator : AbstractValidator<CancelSaleItemCommand>
    {
        public CancelSaleItemValidator()
        {
            RuleFor(x => x.SaleId).NotEmpty().WithMessage("O ID da Venda é obrigatório.");
            RuleFor(x => x.ItemId).NotEmpty().WithMessage("O ID do Item é obrigatório.");
        }
    }
}
