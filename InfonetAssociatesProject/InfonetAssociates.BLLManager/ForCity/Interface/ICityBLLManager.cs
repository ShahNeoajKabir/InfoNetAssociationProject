using InfonetAssociates.Infrustructure.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfonetAssociates.BLLManager.ForCity.Interface
{
    public interface ICityBLLManager
    {
        Task<bool> AddCity(CityViewModel model);
        Task<IEnumerable<CityViewModel>> GetAllCity();
        Task<IEnumerable<CityViewModel>> GetAllCityByCountry(int countryId);
    }
}
