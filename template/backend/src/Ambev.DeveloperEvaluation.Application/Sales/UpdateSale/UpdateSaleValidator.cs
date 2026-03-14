using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Validador para garantir que os dados de atualização estão corretos.
    /// </summary>
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("O ID da Venda é obrigatório.");
            RuleFor(x => x.SaleNumber).NotEmpty().WithMessage("O número da Venda é obrigatório.");
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("O ID do Cliente é obrigatório.");
            RuleFor(x => x.BranchId).NotEmpty().WithMessage("O ID da Filial é obrigatório.");
            RuleFor(x => x.Items).NotEmpty().WithMessage("A Venda tem de conter pelo menos um item.");
        }
    }
}
