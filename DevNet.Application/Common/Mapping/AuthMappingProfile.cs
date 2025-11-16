using AutoMapper;
using DevNet.Application.DTO.Auth;
using DevNet.Application.Features.RefreshTokens.Commands.GetByEmail;
using DevNet.Domain.Entities;

namespace DevNet.Application.Common.Mapping
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<ApplicationUser, TokenUserDto>()
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("FirstName", opt => opt.MapFrom(src => src.FirstName))
                .ForCtorParam("LastName", opt => opt.MapFrom(src => src.LastName))
                .ForCtorParam("Email", opt => opt.MapFrom(src => src.Email))
                .ForCtorParam("Role", opt => opt.MapFrom(src => src.Role.ToString()));

            CreateMap<ApplicationUser, ApplicationUserDto>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<RegisterRequest, ApplicationUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<RefreshTokens, RefreshTokenDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ApplicationUserId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken))
                .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => src.ExpiresAt))
                .ForMember(dest => dest.IsRevoked, opt => opt.MapFrom(src => src.IsRevoked));
        }
    }
}
