using System.Threading.Tasks;
using System.Collections.Generic;
using Data;
using Common.Dto;
using Common.Resources;
using Business.Services.Interfaces;
using Common.Helpers;
using Common;

namespace Business.Services
{
    public class EmailService : ServiceBase, IEmailService
    {
        private readonly AppDbContext _context;
        private readonly LocService _localizer;
        private readonly string emailTo = HttpHelper.GetConfig<string>("EmailSettings:EmailTo");

        public EmailService(AppDbContext parContext, LocService parLocalizer)
            : base(parContext, parLocalizer)
        {
            _context = parContext;
            _localizer = parLocalizer;
        }

        #region IGeneralService Members

        #endregion
    }
}