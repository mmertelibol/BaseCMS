using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Data;
using Common.Dto;
using Common.Resources;
using Business.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace Business.Services
{
    public class GeneralService : ServiceBase, IGeneralService
    {
        private readonly AppDbContext _context;
        private readonly LocService _localizer;

        public GeneralService(AppDbContext parContext, LocService parLocalizer)
            : base(parContext, parLocalizer)
        {
            _context = parContext;
            _localizer = parLocalizer;
        }

        #region IGeneralService Members

        public async Task<List<LanguageDto>> ListLanguages()
        {
          var list = await _context.Languages
                .Where(c => c.IsActive && !c.IsDeleted)
                .ProjectTo<LanguageDto>()
                .ToListAsync();

            return list;
        }

        #endregion
    }
}