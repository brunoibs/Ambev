using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.ListSale;
using Ambev.DeveloperEvaluation.Application.Sale.Create;
using Ambev.DeveloperEvaluation.Application.Sale.Get;
using Ambev.DeveloperEvaluation.Application.Sale.Delete;
using Ambev.DeveloperEvaluation.Application.Sale.List;
using FluentValidation;
using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale;

/// <summary>
/// Controller para gerenciar operações de vendas
/// </summary>
[ApiController]
[Route("api/sale")] // Rota estática específica
public class SaleController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Inicializa uma nova instância de SaleController
    /// </summary>
    /// <param name="mediator">A instância do mediator</param>
    /// <param name="mapper">A instância do AutoMapper</param>
    public SaleController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Cria uma nova venda
    /// </summary>
    /// <param name="request">A requisição de criação da venda</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Os detalhes da venda criada</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validator = new CreateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new ValidationErrorDetail
                {
                    Error = e.PropertyName,
                    Detail = e.ErrorMessage
                }).ToList();

                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Dados inválidos na requisição",
                    Errors = errors
                });
            }

            var command = _mapper.Map<CreateSaleCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
            {
                Success = true,
                Message = "Venda criada com sucesso",
                Data = _mapper.Map<CreateSaleResponse>(response)
            });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = "Erro de validação",
                Errors = new List<ValidationErrorDetail>
                {
                    new ValidationErrorDetail
                    {
                        Error = "ValidationError",
                        Detail = ex.Message
                    }
                }
            });
        }
        catch (Exception ex)
        {
            // Log do erro para debug
            Console.WriteLine($"Erro na criação da venda: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = "Erro interno do servidor",
                Errors = new List<ValidationErrorDetail>
                {
                    new ValidationErrorDetail
                    {
                        Error = "InternalError",
                        Detail = ex.Message
                    }
                }
            });
        }
    }

    /// <summary>
    /// Busca uma venda pelo ID
    /// </summary>
    /// <param name="id">O identificador único da venda</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Os detalhes da venda se encontrada</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetSaleRequest { Id = id };
        var validator = new GetSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetSaleCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetSaleResponse>
        {
            Success = true,
            Message = "Venda encontrada com sucesso",
            Data = _mapper.Map<GetSaleResponse>(response)
        });
    }

    /// <summary>
    /// Lista todas as vendas
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista de todas as vendas</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<ListSaleResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListSales(CancellationToken cancellationToken)
    {
        var query = new ListSaleQuery();
        var response = await _mediator.Send(query, cancellationToken);

        var listResponse = new ListSaleResponse
        {
            Sales = _mapper.Map<List<SaleListItemResponse>>(response.Sales)
        };

        return Ok(new ApiResponseWithData<ListSaleResponse>
        {
            Success = true,
            Message = "Vendas listadas com sucesso",
            Data = listResponse
        });
    }

    /// <summary>
    /// Deleta uma venda pelo ID
    /// </summary>
    /// <param name="id">O identificador único da venda a ser deletada</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta de sucesso se a venda foi deletada</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteSaleRequest { Id = id };
        var validator = new DeleteSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteSaleCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Venda deletada com sucesso"
        });
    }
} 