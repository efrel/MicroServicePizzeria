using MediatR;
using AutoMapper;

using Application.MSPizzeria.DTO.ViewModel.v1;
using Application.MSPizzeria.Validator;
using Domain.MSPizzeria.Interface;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Commands.Product.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<ProductDTO>>
{
    private readonly IProductDomain _productDomain;
    private readonly IMapper _mapper;
    private readonly UpdateProductDTO_Validator _updateProductDtoValidator;
    private readonly IAppLogger<UpdateProductCommandHandler> _logger;

    public UpdateProductCommandHandler(
        IProductDomain productDomain
        , IMapper mapper
        , UpdateProductDTO_Validator updateProductDtoValidator
        , IAppLogger<UpdateProductCommandHandler> logger
    )
    {
        _productDomain = productDomain;
        _mapper = mapper;
        _updateProductDtoValidator = updateProductDtoValidator;
        _logger = logger;
    }
    
    
    public async Task<Response<ProductDTO>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var objParams = request.objParams;
        var objResponse = new Response<ProductDTO>();

        try
        {
            #region VALIDACION DEL DTO
            var objValidacion = await _updateProductDtoValidator.ValidateAsync(objParams);

            if (!objValidacion.IsValid)
            {
                objResponse.IsSuccess = false;
                objResponse.Message = "Alguno de los datos ingresados no son validos.";
                objResponse.Error = objValidacion.Errors;
                return objResponse;
            }
            #endregion
            
            #region MAPEAR DTO
            var mappedProduct = _mapper.Map<Domain.MSPizzeria.Entity.Models.v1.Product>(objParams);
            #endregion

            #region ACTUALIZAR PRODUCTO

            var updateProduct = await _productDomain.UpdateAsync(mappedProduct);
            
            if (updateProduct == null)
            {
                #region REGISTRO DE LOG INFORMATIVO
                _logger.LogInformation("Producto no encontrado");
                #endregion
                
                objResponse.IsSuccess = false;
                objResponse.Message = "Producto no encontrado";
                return objResponse;
            }
            
            
            var productDto = _mapper.Map<ProductDTO>(updateProduct);

            #endregion
            
            #region EN CASO DE EXITO
            objResponse.IsSuccess = true;
            objResponse.Data = productDto;
            objResponse.Message = "Consulta Exitosa";
            #endregion

            #region REGISTRO DE LOG INFORMATIVO
            _logger.LogInformation("Consulta Exitosa");
            #endregion
        }
        catch (Exception e)
        {
            #region SI SUCEDIO UN PROBLEMA
            objResponse.IsSuccess = false;
            objResponse.Message = "Sucedio algun imprevisto al procesar su solicitud.";
            #endregion

            #region REGISTRO DE LOG DE ERROR
            _logger.LogError("UpdateProductCommandHandler", e);
            #endregion
        }

        return objResponse;
    }
}