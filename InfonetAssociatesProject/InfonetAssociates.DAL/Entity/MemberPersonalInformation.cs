using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfonetAssociates.DAL.Entity
{
    public class MemberPersonalInformation
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string MemberName { get; set; }
        public string Date { get; set; }
        public string Documents { get; set; }
        [NotMapped]
        public string? ImageBase64 { get; set; }
        public ICollection <MemberPersonLanguage> MemberPersonLanguage { get; set; }
    }
}
