using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfonetAssociates.Infrustructure.BusinessObjects
{
    public class MemberPersonalInformationViewModel
    {

        public int Id { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string? MemberName { get; set; }
        public string? Date { get; set; }
        public string? Documents { get; set; }
        public string? CountryName { get; set; }
        public string? CityName { get; set; }
        public string? Language{ get; set; }
        public IFormFile ImageFile { get; set; }
        public List<LanguageViewModel> languageList { get; set; }

    }

    public class MemberPersonalLanguageViewModel
    {

        public int Id { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public string? MemberName { get; set; }
        public string? SkillName { get; set; }
        public string? Documents { get; set; }
        public string? CountryName { get; set; }
        public string? CityName { get; set; }
        public string? Date { get; set; }
        public string? LanguageSkill { get; set; }
        public string? ImageBase64 { get; set; }

    }


    public class PersonalLanguageViewModel
    {

        public int Id { get; set; }
        public int MemberPersonInfoId { get; set; }
        public int? LanguageId { get; set; }
        public List<LanguageViewModel> languageList { get; set; }

    }

    public class MemberPersonalInfoLanguageViewModel
    {

        public MemberPersonalLanguageViewModel MemberPersonalLanguageViewModel { get; set; }
        public List<LanguageViewModel>? languageList { get; set; }

    }
}
