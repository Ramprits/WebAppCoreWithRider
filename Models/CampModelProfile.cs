using AutoMapper;
using WebAppCore.Entities;
using WebAppCore.Models.CampModels;

namespace WebAppCore.Models
{
    public class CampModelProfile : Profile
    {
        public CampModelProfile()
        {
            CreateMap <Camp, CampModel>()
                .ForMember(c => c.StartDate,
                    opt => opt.MapFrom(e => e.EventDate))
                .ForMember(c => c.EndDate,
                    opt => opt.MapFrom(e => e.EventDate)).ReverseMap();
        }
    }
}
