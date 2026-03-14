using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    /// <summary>
    /// Controller responsável pelas operações de Vendas.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Recupera uma Venda pelo seu ID
        /// </summary>
        /// <param name="id">O ID único da venda</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Os detalhes da venda caso exista</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetSaleRequest { Id = id };

            var command = new GetSaleCommand(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<GetSaleResponse>
            {
                Success = true,
                Message = "Venda recuperada com sucesso.",
                Data = _mapper.Map<GetSaleResponse>(response)
            });
        }

        /// <summary>
        /// Cria uma nova Venda.
        /// </summary>
        /// <param name="request">Os dados da venda.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Os dados resumidos da venda criada.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateSaleCommand>(request);

            var result = await _mediator.Send(command, cancellationToken);

            var response = _mapper.Map<CreateSaleResponse>(result);

            return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
            {
                Success = true,
                Message = "Sale created successfully",
                Data = response
            });
        }

        /// <summary>
        /// Atualiza uma Venda existente.
        /// </summary>
        /// <param name="id">O ID único da venda a ser atualizada</param>
        /// <param name="request">Os novos dados da venda</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Os detalhes resumidos da venda atualizada</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSale([FromRoute] Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateSaleCommand>(request);
            command.Id = id;

            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<UpdateSaleResponse>
            {
                Success = true,
                Message = "Venda atualizada com sucesso.",
                Data = _mapper.Map<UpdateSaleResponse>(response)
            });
        }

        /// <summary>
        /// Apaga ou Cancela uma Venda.
        /// </summary>
        /// <param name="id">O ID único da venda a ser apagada/cancelada</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Indicador de sucesso da operação</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<DeleteSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteSaleRequest { Id = id };
            var command = _mapper.Map<DeleteSaleCommand>(request);

            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<DeleteSaleResponse>
            {
                Success = true,
                Message = "Venda apagada/cancelada com sucesso.",
                Data = _mapper.Map<DeleteSaleResponse>(response)
            });
        }

        /// <summary>
        /// Cancela um Item específico de uma Venda.
        /// </summary>
        /// <param name="id">O ID único da venda</param>
        /// <param name="itemId">O ID único do item da venda a ser cancelado</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Indicador de sucesso da operação</returns>
        [HttpDelete("{id}/items/{itemId}")]
        [ProducesResponseType(typeof(ApiResponseWithData<Features.Sales.CancelSaleItem.CancelSaleItemResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelSaleItem([FromRoute] Guid id, [FromRoute] Guid itemId, CancellationToken cancellationToken)
        {
            var command = new Application.Sales.CancelSaleItem.CancelSaleItemCommand(id, itemId);

            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<Features.Sales.CancelSaleItem.CancelSaleItemResponse>
            {
                Success = true,
                Message = "Item da venda cancelado com sucesso.",
                Data = _mapper.Map<Features.Sales.CancelSaleItem.CancelSaleItemResponse>(response)
            });
        }
    }
}
