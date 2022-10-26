using InfonetAssociates.Infrustructure.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfonetAssociates.BLLManager.ForLanguage.Interface
{
    public interface ILanguageBLLManager
    {
        Task<bool> AddLanguage(LanguageViewModel model);
        Task<IEnumerable<LanguageViewModel>> GetAllLanguage();
    }
}
