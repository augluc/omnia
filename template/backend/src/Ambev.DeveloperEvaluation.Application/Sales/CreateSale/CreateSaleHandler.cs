using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Handler responsável por processar o comando CreateSaleCommand.
    /// </summary>
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = new Sale(
                request.SaleNumber,
                request.SaleDate == default ? DateTime.UtcNow : request.SaleDate,
                request.CustomerId,
                request.CustomerName,
                request.BranchId,
                request.BranchName
            );

            foreach (var itemCommand in request.Items)
            {
                var saleItem = new SaleItem(
                    itemCommand.ProductId,
                    itemCommand.ProductName,
                    itemCommand.Quantity,
                    itemCommand.UnitPrice
                );

                sale.AddItem(saleItem);
            }

            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

            return _mapper.Map<CreateSaleResult>(createdSale);
        }
    }
}
