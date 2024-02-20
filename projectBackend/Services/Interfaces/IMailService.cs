using projectBackend.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Services.Interfaces
{
    public interface IMailService
    {
        Task SendResetEmailAsync(MailDTO mailRequestModel);
    }
}
