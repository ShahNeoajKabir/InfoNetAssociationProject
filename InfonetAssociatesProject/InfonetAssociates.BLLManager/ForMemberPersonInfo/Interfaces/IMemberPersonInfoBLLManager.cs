using InfonetAssociates.Infrustructure.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfonetAssociates.BLLManager.ForMemberPersonInfo.Interfaces
{
    public interface IMemberPersonInfoBLLManager
    {
        Task<int> AddPersonalInfo(MemberPersonalInformationViewModel model);
        Task<bool> AddPersonalLanguage(PersonalLanguageViewModel model);
        Task<IEnumerable<MemberPersonalLanguageViewModel>> GetAllAddPersonalInfo();
        Task<bool> DeleteMemberById(int Id);
        Task<MemberPersonalInfoLanguageViewModel> GetMemberById(int Id);

    }
}
