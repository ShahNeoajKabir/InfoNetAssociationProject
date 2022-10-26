using InfonetAssociates.Infrustructure.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfonetAssociates.BLLManager.ForCountry.Interface
{
    public interface ICountryBLLManager
    {
        Task<bool> AddCountry(CountryViewModel model);
        Task<IEnumerable<CountryViewModel>> GetAllCountry();
        Task<bool> DeleteCountry(int Id);
    }
}
