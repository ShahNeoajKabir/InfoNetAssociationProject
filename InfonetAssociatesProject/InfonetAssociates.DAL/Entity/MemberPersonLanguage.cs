using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfonetAssociates.DAL.Entity
{
    public class MemberPersonLanguage
    {
        public int Id { get; set; }
        public int MemberPersonInfoId { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public MemberPersonalInformation MemberPersonalInformation { get; set; }
    }
}
