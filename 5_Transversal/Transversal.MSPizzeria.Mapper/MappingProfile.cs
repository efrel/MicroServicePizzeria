using AutoMapper;

using Application.MSPizzeria.DTO.ViewModel.v1;
using Domain.MSPizzeria.Entity.Models.v1;

namespace Transversal.MSPizzeria.Mapper;

public class MappingProfile : Profile
{

    #region CONSTRUCTOR

    public MappingProfile()
    {
        //Habilita hacer el Mapeo 
        #region MAPEO DE CAMPOS IGUALES

        CreateMap<UserInfoModel, UserInfoDTO>().ReverseMap();

        CreateMap<Product, ProductDTO>().ReverseMap();
        
        CreateMap<Product, CreateProductDTO>().ReverseMap();
        CreateMap<Product, UpdateProductDTO>().ReverseMap();

        #endregion
        
        
        #region MAPEO DE OBJETOS CON CAMPOS DISTINTOS
        
        CreateMap<UserInfoModel, RegisterRequestDTO>().ReverseMap()
            .ForMember(dest  => dest.Email, opt => opt.MapFrom(src => src.Email));

        CreateMap<RegisterRequestDTO, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            // Configurar valores por defecto para campos requeridos
            .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => false))
            // No mapear directamente el password, se maneja en el handler
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            // Ignorar campos que no deberían mapearse directamente
            .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
            .ForMember(dest => dest.LockoutEnabled, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.AccessFailedCount, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.TwoFactorEnabled, opt => opt.MapFrom(src => false))
            // Campos de Identity que deben ser ignorados
            .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
            .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore());
        
        CreateMap<ApplicationUserDTO, ApplicationUser>().ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

        #endregion
    }

    #endregion

}