using AutoMapper;
using AutoMapper.QueryableExtensions;
using InfonetAssociates.BLLManager.ForLanguage.Interface;
using InfonetAssociates.DAL.DatabaseContexts;
using InfonetAssociates.DAL.Entity;
using InfonetAssociates.Infrustructure.BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfonetAssociates.BLLManager.ForLanguage.Logic
{
    public class LanguageBLLManager:ILanguageBLLManager
    { 
    private readonly DatabaseContext _databaseContext;
    private readonly IMapper _mapper;

    public LanguageBLLManager(DatabaseContext databaseContext, IMapper mapper)
    {
        _databaseContext = databaseContext;
        _mapper = mapper;
    }

    public async Task<bool> AddLanguage(LanguageViewModel model)
    {
        try
        {
            Language data;
            data = await _databaseContext.Language.FirstOrDefaultAsync(p => p.Id == model.Id);
            if (data == null)
            {
                data = new Language();
                await _databaseContext.Language.AddAsync(data);
            }

            if (_databaseContext.Language.Any(p => p.LanguageName.ToLower() == model.LanguageName.ToLower() && p.Id != model.Id
        ))
                throw new DuplicateWaitObjectException("Name", model.LanguageName);
            data = _mapper.Map((LanguageViewModel)model, data);

            return await _databaseContext.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<LanguageViewModel>> GetAllLanguage()
    {
        try
        {
            IQueryable<LanguageViewModel> languageList = _databaseContext.Language
            .ProjectTo<LanguageViewModel>(_mapper.ConfigurationProvider);
            return languageList;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
}
