using AutoMapper;
using AutoMapper.QueryableExtensions;
using InfonetAssociates.BLLManager.ForCountry.Interface;
using InfonetAssociates.DAL.DatabaseContexts;
using InfonetAssociates.DAL.Entity;
using InfonetAssociates.Infrustructure.BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfonetAssociates.BLLManager.ForCountry.Logic
{
    public class CountryBLLManager : ICountryBLLManager
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public CountryBLLManager(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<bool> AddCountry(CountryViewModel model)
        {
            try
            {
                Country data;
                data =await _databaseContext.Country.FirstOrDefaultAsync(p => p.Id == model.Id);
                if (data == null)
                {
                    data = new Country();
                    await _databaseContext.Country.AddAsync(data);
                }

                if (_databaseContext.Country.Any(p => p.CountryName.ToLower() == model.CountryName.ToLower() && p.Id != model.Id
            ))
                    throw new DuplicateWaitObjectException("Name", model.CountryName);
                data = _mapper.Map((CountryViewModel)model, data);

                return await _databaseContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CountryViewModel>> GetAllCountry()
        {
            try
            {
                IEnumerable<CountryViewModel> countryList =await _databaseContext.Country
                .ProjectTo<CountryViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
                return countryList;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCountry(int Id)
        {
            try
            {
                Country? data = new Country();
                data = await _databaseContext.Country.FindAsync(Id);
                await _databaseContext.Database.BeginTransactionAsync();
                 _databaseContext.Country.Remove(data);
                var result=await _databaseContext.SaveChangesAsync();
                await _databaseContext.Database.CommitTransactionAsync();
                return result>0;
            }
            catch (Exception ex)
            {
                await _databaseContext.Database.RollbackTransactionAsync();

                throw new Exception(ex.Message);
            }
        }
    }
}
