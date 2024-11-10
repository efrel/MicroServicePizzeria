using MediatR;
using AutoMapper;

// MIS REFERENCIAS
using Application.MSPizzeria.DTO.ViewModel.v1;
using Application.MSPizzeria.Validator;
using Domain.MSPizzeria.Interface;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Commands.Product.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<ProductDTO>>
{
    private readonly IProductDomain _productDomain;
    private readonly IMapper _mapper;
    private readonly CreateProductDTO_Validator _createProductDtoValidator;
    private readonly IAppLogger<CreateProductCommandHandler> _logger;

    public CreateProductCommandHandler(
        IProductDomain productDomain
        , IMapper mapper
        , CreateProductDTO_Validator createProductDtoValidator
        , IAppLogger<CreateProductCommandHandler> logger
    )
    {
        _productDomain = productDomain;
        _mapper = mapper;
        _createProductDtoValidator = createProductDtoValidator;
        _logger = logger;
    }
    
    public async Task<Response<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var objParams = request.objParams;
        var objResponse = new Response<ProductDTO>();

        try
        {
            #region VALIDACION DEL DTO
            var objValidacion = await _createProductDtoValidator.ValidateAsync(objParams);

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

            #region CREAR PRODUCTO

            var createdProduct = await _productDomain.CreateAsync(mappedProduct);
            var productDto = _mapper.Map<ProductDTO>(createdProduct);

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
            _logger.LogError("CreateProductCommandHandler", e);
            #endregion
        }

        return objResponse;
    }
}