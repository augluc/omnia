using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem
{
    public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, CancelSaleItemResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<CancelSaleItemHandler> _logger;

        public CancelSaleItemHandler(
            ISaleRepository saleRepository,
            ILogger<CancelSaleItemHandler> logger)
        {
            _saleRepository = saleRepository;
            _logger = logger;
        }

        public async Task<CancelSaleItemResult> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new CancelSaleItemValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"Venda com o ID {request.SaleId} não foi encontrada.");

            sale.CancelItem(request.ItemId);

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            _logger.LogInformation(
                "--- EVENTO DE DOMÍNIO PUBLICADO --- | SaleItemCancelledEvent disparado para a Venda ID: {SaleId}, Item ID: {ItemId}.",
                request.SaleId, request.ItemId);

            return new CancelSaleItemResult { Success = true };
        }
    }
}
