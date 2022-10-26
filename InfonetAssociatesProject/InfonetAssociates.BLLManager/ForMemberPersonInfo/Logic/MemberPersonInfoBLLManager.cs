using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dapper;
using InfonetAssociates.BLLManager.ForMemberPersonInfo.Interfaces;
using InfonetAssociates.DAL.DatabaseContexts;
using InfonetAssociates.DAL.Entity;
using InfonetAssociates.Infrustructure;
using InfonetAssociates.Infrustructure.BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfonetAssociates.BLLManager.ForMemberPersonInfo.Logic
{
    public class MemberPersonInfoBLLManager: IMemberPersonInfoBLLManager
    {
        private readonly IConfiguration _config;
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public MemberPersonInfoBLLManager(DatabaseContext databaseContext, IConfiguration config, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _config = config;
            _mapper = mapper;
        }

        public async  Task<int> AddPersonalInfo(MemberPersonalInformationViewModel model)
        {
            try
            {
                _databaseContext.Database.BeginTransaction();
                string _filePath = Upload(model.ImageFile, _config["Directory:WebsiteSlider"]);
                MemberPersonalInformation data;
                data = await _databaseContext.MemberPersonalInformation.FirstOrDefaultAsync(p => p.Id == model.Id);
                if (data == null)
                {
                    data = new MemberPersonalInformation();
                    await _databaseContext.MemberPersonalInformation.AddAsync(data);
                }
                model.Documents = _filePath;
                data = _mapper.Map((MemberPersonalInformationViewModel)model, data);
                var result = await _databaseContext.SaveChangesAsync()>0;
                await _databaseContext.Database.CommitTransactionAsync();
                return data.Id;
            }
            catch (Exception ex)
            {
                await _databaseContext.Database.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<MemberPersonalLanguageViewModel>> GetAllAddPersonalInfo()
        {
            try
            {
                string path = _config["Directory:WebsiteSlider"];
                IEnumerable<MemberPersonalInformationViewModel> list;
                var memberPersonLanguage = await _databaseContext.MemberPersonLanguage.ToListAsync();
                var language = await _databaseContext.Language.ToListAsync();
                var memberPersonalInformation = await _databaseContext.MemberPersonalInformation.ToListAsync();
                foreach (var item in memberPersonalInformation)
                {
                    if (File.Exists(path + item.Documents))
                    {
                        var bytes = File.ReadAllBytes(path + item.Documents);
                        item.ImageBase64 = Convert.ToBase64String(bytes);
                    }
                  
                }
                var uu= memberPersonLanguage.GroupJoin(language, i => i.LanguageId, o => o.Id, (o, i) => new { o, i })
                    .Select(x => new { MemberPersonInfoId = x.o.MemberPersonInfoId, LanguageName = string.Join(",", x.i.Select(y => y.LanguageName)) });

                var result = memberPersonalInformation.GroupJoin(memberPersonLanguage, acc => acc.Id, accrol => accrol.MemberPersonInfoId,
            (o, i) => new { o, list = i })
        .GroupJoin(uu, ii => ii.o.Id, oo => oo.MemberPersonInfoId,
            (ii, oo) => new MemberPersonalLanguageViewModel
            {
                Id = ii.o.Id,
                MemberName = ii.o.MemberName,
                Documents= ii.o.Documents,
                Date= ii.o.Date,
                CityName = _databaseContext.City.Where(p => p.Id == ii.o.CityId).FirstOrDefault().CityName,
                CountryName = _databaseContext.Country.Where(p => p.Id == ii.o.CountryId).FirstOrDefault().CountryName,
                ImageBase64 = ii.o.ImageBase64,
                LanguageSkill = string.Join(",", oo.Select(x => x.LanguageName))
            });

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddPersonalLanguage(PersonalLanguageViewModel model)
        {
            try
            {
                _databaseContext.Database.BeginTransaction();
              
                foreach (var item in model.languageList)
                {
                    MemberPersonLanguage personLanguage = new MemberPersonLanguage()
                    {
                        LanguageId = item.Id,
                        MemberPersonInfoId = model.MemberPersonInfoId
                    };

                    await _databaseContext.MemberPersonLanguage.AddRangeAsync(personLanguage);
                }


                var result = await _databaseContext.SaveChangesAsync() > 0;
                await _databaseContext.Database.CommitTransactionAsync();
                return result;
            }
            catch (Exception ex)
            {
                await _databaseContext.Database.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        private string Upload(IFormFile file, string path)
        {
            try
            {
                string filePath = null;
                string fileName = new String(Path.GetFileNameWithoutExtension(file.FileName).Take(10).ToArray()).Replace(" ", "-");
                fileName = fileName + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                string fileExtension = Path.GetExtension(file.FileName);
                filePath = path + fileName;
                using (var stream = File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                return fileName;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<bool> DeleteMemberById(int Id)
        {
            try
            {
                await _databaseContext.Database.BeginTransactionAsync();
                var memberInfo = _databaseContext.MemberPersonalInformation.Single(a => a.Id == Id);
                var memberSkill = _databaseContext.MemberPersonLanguage.Where(b => EF.Property<int>(b, "MemberPersonInfoId") == Id);
                foreach (var skill in memberSkill)
                {
                    memberInfo.MemberPersonLanguage.Remove(skill);
                }
                _databaseContext.Remove(memberInfo);
                var result = await _databaseContext.SaveChangesAsync();
                await _databaseContext.Database.CommitTransactionAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                await _databaseContext.Database.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<MemberPersonalInfoLanguageViewModel> GetMemberById(int Id)
        {
            try
            {
                MemberPersonalInfoLanguageViewModel result = new MemberPersonalInfoLanguageViewModel() ;
                
                using (var con = new SqlConnection(Connection.ConnectionString()))
                {
                    result.MemberPersonalLanguageViewModel = con.QuerySingle<MemberPersonalLanguageViewModel>("GetMemberPersonById", param: new { Id = Id }, commandType: CommandType.StoredProcedure);
                    result.languageList = con.Query<LanguageViewModel>("GetLanguageById", param: new { Id = Id }, commandType: CommandType.StoredProcedure).ToList();
                }
                return result;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


    }
}
