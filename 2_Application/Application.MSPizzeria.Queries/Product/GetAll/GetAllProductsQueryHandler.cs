using MediatR;
using AutoMapper;

using Application.MSPizzeria.DTO.ViewModel.v1;
using Domain.MSPizzeria.Interface;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Queries.Product.GetAll;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Response<List<ProductDTO>>>
{
    private readonly IProductDomain _productDomain;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetAllProductsQueryHandler> _logger;

    public GetAllProductsQueryHandler(IProductDomain productDomain, IMapper mapper, IAppLogger<GetAllProductsQueryHandler> logger)
    {
        _productDomain = productDomain;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<Response<List<ProductDTO>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var objParams = request.objParams;
        var objResponse = new Response<List<ProductDTO>>();

        try
        {
            #region OBTENER DATOS

            var products = await _productDomain.GetByFilterAsync(objParams.IsAvailable, objParams.Category);
            var productsDto = _mapper.Map<List<ProductDTO>>(products);

            #endregion
            
            #region EN CASO DE EXITO
            objResponse.IsSuccess = true;
            objResponse.Data = productsDto;
            objResponse.Message = "Login Exitoso";
            #endregion

            #region REGISTRO DE LOG INFORMATIVO
            _logger.LogInformation("Login Exitoso");
            #endregion
        }
        catch (Exception e)
        {
            #region SI SUCEDIO UN PROBLEMA
            objResponse.IsSuccess = false;
            objResponse.Message = "Sucedio algo imprevisto al procesar su solicitud.";
            #endregion

            #region REGISTRO DE LOG DE ERROR
            _logger.LogError("GetAllProductsQueryHandler", e);
            #endregion
        }
        
        return objResponse;
    }
}