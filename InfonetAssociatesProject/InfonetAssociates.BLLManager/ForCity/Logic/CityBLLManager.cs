using AutoMapper;
using AutoMapper.QueryableExtensions;
using InfonetAssociates.BLLManager.ForCity.Interface;
using InfonetAssociates.DAL.DatabaseContexts;
using InfonetAssociates.DAL.Entity;
using InfonetAssociates.Infrustructure.BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfonetAssociates.BLLManager.ForCity.Logic
{
    public class CityBLLManager:ICityBLLManager
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public CityBLLManager(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<bool> AddCity(CityViewModel model)
        {
            try
            {
                City data;
                data = await _databaseContext.City.FirstOrDefaultAsync(p => p.Id == model.Id);
                if (data == null)
                {
                    data = new City();
                    await _databaseContext.City.AddAsync(data);
                }

                if (_databaseContext.City.Any(p => p.CityName.ToLower() == model.CityName.ToLower() && p.Id != model.Id
            ))
                    throw new DuplicateWaitObjectException("Name", model.CityName);
                data = _mapper.Map((CityViewModel)model, data);

                return await _databaseContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CityViewModel>> GetAllCity()
        {
            try
            {
                IEnumerable<CityViewModel> countryList = await _databaseContext.City
                .ProjectTo<CityViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
                return countryList;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public async Task<IEnumerable<CityViewModel>> GetAllCityByCountry(int countryId)
        {
            try
            {
                IEnumerable<CityViewModel> countryList = await _databaseContext.City.Where(p=>p.CountryId==countryId)
                .ProjectTo<CityViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
                return countryList;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
