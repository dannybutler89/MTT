using AutoMapper;
using Domain.Entities;
using Models.Response;

namespace Domain.Maps
{
    public class CompanyMappings : Profile
    {
        public CompanyMappings()
        {
            CreateMap<Company, CompanyResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address1))
                .ForMember(dest => dest.Address2, opt => opt.MapFrom(src => src.Address2))
                .ForMember(dest => dest.Address3, opt => opt.MapFrom(src => src.Address3))
                .ForMember(dest => dest.Postcode, opt => opt.MapFrom(src => src.Postcode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.InsuranceEndDate, opt => opt.MapFrom(src => src.InsuranceEndDate))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.HasActivePolicy, opt => opt.Ignore());
        }
    }
}
