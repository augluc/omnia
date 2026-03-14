using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Handler responsável por processar o DeleteSaleCommand.
    /// </summary>
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<DeleteSaleHandler> _logger;

        public DeleteSaleHandler(
            ISaleRepository saleRepository,
            ILogger<DeleteSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _logger = logger;
        }

        public async Task<DeleteSaleResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingSale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingSale == null)
                throw new KeyNotFoundException($"Venda com o ID {request.Id} não foi encontrada.");

            var deleted = await _saleRepository.DeleteAsync(request.Id, cancellationToken);
            if (!deleted)
                return new DeleteSaleResult { Success = false };

            _logger.LogInformation(
                "--- EVENTO DE DOMÍNIO PUBLICADO --- | SaleCancelledEvent disparado para a Venda ID: {SaleId}.",
                request.Id);

            return new DeleteSaleResult { Success = true };
        }
    }
}
