using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Handler responsável por processar o GetSaleCommand.
    /// </summary>
    public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSaleHandler> _logger;

        public GetSaleHandler(
            ISaleRepository saleRepository,
            IMapper mapper,
            ILogger<GetSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetSaleResult> Handle(GetSaleCommand request, CancellationToken cancellationToken)
        {
            // 1. Validação do pedido
            var validator = new GetSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            // 2. Busca no repositório
            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

            if (sale == null)
                throw new KeyNotFoundException($"Venda com o ID {request.Id} não foi encontrada.");

            // 3. Mapeamento
            var result = _mapper.Map<GetSaleResult>(sale);

            // 4. Log para acompanhar a operação (simples, útil para debug)
            _logger.LogInformation("A Venda {SaleId} foi consultada com sucesso.", request.Id);

            return result;
        }
    }
}
