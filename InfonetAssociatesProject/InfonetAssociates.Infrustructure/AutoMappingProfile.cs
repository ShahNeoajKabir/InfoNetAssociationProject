

using AutoMapper;
using InfonetAssociates.DAL.Entity;
using InfonetAssociates.Infrustructure.BusinessObjects;

namespace InfonetAssociates.Infrustructure
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Country, CountryViewModel>();
            CreateMap<CountryViewModel, Country>();

            CreateMap<Language, LanguageViewModel>();
            CreateMap<LanguageViewModel, Language>();

            CreateMap<MemberPersonalInformation, MemberPersonalInformationViewModel>();
            CreateMap<MemberPersonalInformationViewModel, MemberPersonalInformation>();
            CreateMap<PersonalLanguageViewModel, MemberPersonLanguage>();
            CreateMap<MemberPersonLanguage, PersonalLanguageViewModel>();

            CreateMap<CityViewModel, City>();
            CreateMap<City, CityViewModel>()
                .ForMember(d => d.CountryName, s => s.MapFrom(x => x.Country.CountryName));

        }
    }
}
