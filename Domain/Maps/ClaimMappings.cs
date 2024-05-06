using AutoMapper;
using Domain.Entities;
using Models.Request;
using Models.Response;
namespace Domain.Maps
{
    public class ClaimMappings : Profile
    {
        public ClaimMappings()
        {
            CreateMap<Claim, ClaimResponse>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AssuredName, opt => opt.MapFrom(src => src.AssuredName))
                .ForMember(dest => dest.IncurredLoss, opt => opt.MapFrom(src => src.IncurredLoss))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.ClaimDate, opt => opt.MapFrom(src => src.ClaimDate))
                .ForMember(dest => dest.Closed, opt => opt.MapFrom(src => src.Closed))
                .ForMember(dest => dest.LossDate, opt => opt.MapFrom(src => src.LossDate))
                .ForMember(dest => dest.Ucr, opt => opt.MapFrom(src => src.Ucr))
                .ForMember(dest => dest.ClaimType, opt => opt.MapFrom(src => src.ClaimType.Name))
                .ForMember(dest => dest.ClaimAgeDays, opt => opt.Ignore());

            CreateMap<UpdateClaimRequest, Claim>(MemberList.Source)
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.CompanyId, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.AssuredName, opt => opt.MapFrom(src => src.AssuredName))
                .ForMember(dest => dest.Closed, opt => opt.MapFrom(src => src.Closed))
                .ForMember(dest => dest.ClaimDate, opt => opt.MapFrom(src => src.ClaimDate))
                .ForMember(dest => dest.IncurredLoss, opt => opt.MapFrom(src => src.IncurredLoss))
                .ForMember(dest => dest.LossDate, opt => opt.MapFrom(src => src.LossDate))
                .ForMember(dest => dest.Ucr, opt => opt.MapFrom(src => src.Ucr));
        }
    }
}
