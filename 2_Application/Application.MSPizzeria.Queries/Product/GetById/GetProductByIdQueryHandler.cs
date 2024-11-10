using AutoMapper;
using MediatR;

using Application.MSPizzeria.DTO.ViewModel.v1;
using Domain.MSPizzeria.Interface;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Queries.Product.GetById;

public class GetProductByIdQueryHandler  : IRequestHandler<GetProductByIdQuery, Response<ProductDTO>>
{
    private readonly IProductDomain _productDomain;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetProductByIdQueryHandler> _logger;

    public GetProductByIdQueryHandler(IProductDomain productDomain, IMapper mapper, IAppLogger<GetProductByIdQueryHandler> logger)
    {
        _productDomain = productDomain;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<Response<ProductDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var objParams = request.objParams;
        var objResponse = new Response<ProductDTO>();

        try
        {
            #region OBTENER DATOS
            var productModel = await _productDomain.GetByIdAsync(objParams.Id);
            var productDto = _mapper.Map<ProductDTO>(productModel);
            #endregion
            
            #region EN CASO DE EXITO
            objResponse.IsSuccess = true;
            objResponse.Data = productDto;
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