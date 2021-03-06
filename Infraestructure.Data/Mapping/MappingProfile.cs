using AutoMapper;

namespace Infraestructure.Data.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Company, Entities.Company>();
            CreateMap<Entities.Company, Domain.Company>();
            
            CreateMap<Domain.Contact, Entities.Contact>()
                .ForMember(c => c.Birthdate, 
                    m => m.MapFrom(u => DateTime.ParseExact(u.Birthdate, Domain.Contact.DateFormat, null)))
                .ForMember(c => c.ProfileImgUri, m => m.MapFrom(u => u.Uri));
            CreateMap<Entities.Contact, Domain.Contact>()
                .ForMember(c => c.Birthdate,
                    m => m.MapFrom(u => u.Birthdate.ToString(Domain.Contact.DateFormat)))
                .ForMember(c => c.Uri, m => m.MapFrom(u => u.ProfileImgUri));

            CreateMap<Domain.Address, Entities.Address>();
            CreateMap<Entities.Address, Domain.Address>();

            CreateMap<Domain.Phone, Entities.Phone>();
            CreateMap<Entities.Phone, Domain.Phone>();
        }
    }
}
