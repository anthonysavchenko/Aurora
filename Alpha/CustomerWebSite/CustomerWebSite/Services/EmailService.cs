using Microsoft.AspNet.Identity;
using SendGrid;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Taumis.Alpha.Server.Core.Services.Settings;

namespace CustomerWebSite.Services
{
    public class EmailService : IEmailService
    {
        private readonly ISettingsService _settingsService;

        public EmailService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        #region Implementation of IIdentityMessageService

        /// <summary>
        /// This method should send the message
        /// </summary>
        /// <param name="message"/>
        /// <returns/>
        public Task SendAsync(IdentityMessage message)
        {
            SmtpSettings _smtp = _settingsService.GetSmtpSettings();

            SendGridMessage _msg = new SendGridMessage();
            _msg.AddTo(message.Destination);
            _msg.From = new MailAddress(_smtp.SenderEmail, _smtp.SenderName);
            _msg.Subject = message.Subject;
            _msg.Text = message.Body;
            _msg.Html = message.Body;

            Web _transport = new Web(new NetworkCredential(_smtp.Login, _smtp.Password));

            return _transport.DeliverAsync(_msg);
        }

        #endregion
    }
}