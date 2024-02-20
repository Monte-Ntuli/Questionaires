using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using projectBackend.Models.Requests;
using projectBackend.Services.Interfaces;
using projectBackend.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace projectBackend.Services
{
    public class MailService : IMailService 
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendResetEmailAsync(MailDTO mailRequestModel)
        {

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequestModel.Email));
            email.Subject = "Password Reset";
            var builder = new BodyBuilder();
            
            builder.HtmlBody = "Please use the link to reset your password, http://localhost:4200/forgot Ignore if you did request to reset your password";
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        //public async Task SendEmailAsync(MailRequestsModel mailRequest)
        //{
        //    var email = new MimeMessage();
        //    email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        //    email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        //    email.Subject = mailRequest.Subject;
        //    var builder = new BodyBuilder();

        //    if (mailRequest.Attachments != null)
        //    {
        //        byte[] fileBytes;
        //        foreach (var file in mailRequest.Attachments)
        //        {
        //            if (file.Length > 0)
        //            {
        //                using (var ms = new MemoryStream())
        //                {
        //                    file.CopyTo(ms);
        //                    fileBytes = ms.ToArray();
        //                }
        //                builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
        //            }
        //        }
        //    }

        //    builder.HtmlBody = mailRequest.Body;
        //    email.Body = builder.ToMessageBody();
        //    using var smtp = new SmtpClient();
        //    smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        //    smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        //    await smtp.SendAsync(email);
        //    smtp.Disconnect(true);
        //}
    }
}
