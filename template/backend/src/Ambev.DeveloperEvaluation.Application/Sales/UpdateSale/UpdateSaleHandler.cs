using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Handler responsável por processar o UpdateSaleCommand.
    /// </summary>
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSaleHandler> _logger;

        public UpdateSaleHandler(
            ISaleRepository saleRepository,
            IMapper mapper,
            ILogger<UpdateSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingSale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingSale == null)
                throw new KeyNotFoundException($"Venda com o ID {request.Id} não foi encontrada.");

            _mapper.Map(request, existingSale);

            await _saleRepository.UpdateAsync(existingSale, cancellationToken);

            _logger.LogInformation(
                "--- EVENTO DE DOMÍNIO PUBLICADO --- | SaleModifiedEvent disparado para a Venda ID: {SaleId}.",
                existingSale.Id);

            return _mapper.Map<UpdateSaleResult>(existingSale);
        }
    }
}
