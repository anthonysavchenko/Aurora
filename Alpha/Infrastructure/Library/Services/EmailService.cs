using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using Microsoft.Practices.CompositeUI;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Infrastructure.Interface.Services.Settings;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services
{
    public class EmailService : IEmailService
    {
        [ServiceDependency]
        public ISettingsService SettingsSrv { private get; set; }

        /// <summary>
        /// Отправляет учетные данные для доступа на веб сайт абоненту
        /// </summary>
        /// <param name="customerEmail">Email абонента</param>
        /// <param name="customerName">Имя абонента</param>
        /// <param name="password">Пароль</param>
        public void SendCredentials(string customerEmail, string customerName, string password)
        {
            try
            {
                MailMessage _mailMsg = new MailMessage();
                SmtpSettings _set = SettingsSrv.GetSmtpSettings();

                _mailMsg.To.Add(new MailAddress(customerEmail, customerName));
                _mailMsg.From = new MailAddress(_set.SenderEmail, _set.SenderName);

                _mailMsg.Subject = "Ваш логин и пароль";

                string _plainText =
                    string.Format(
@"Уважаемый абонент!

Вам предоставлен доступ к личному кабинету абонента

Ваши учетные данные:
    Логин:  {0}
    Пароль: {1}

Ваша управляющая организация: ООО ""Жилищные услуги""; г. Владивосток, пр-т Острякова, 38; 
Расчетный центр: 246-92-40, 261-95-84, Пн-Пт 8:00-17:00, обед 12-13, среда неприемный день
Главный офис: 246-46-01. Авар. служба 261-47-14, 298-09-81
www.dom-vl.ru

Чтобы открыть личный кабинет, перейдите по ссылке: www.qvitex.ru

Это письмо создано автоматически и не требует ответа!

Qvitex.ru - сервис для управляющих организаций и их абонентов",
                        customerEmail, password);

                string _htmlText =
                    string.Format(
@"<!DOCTYPE html>
<html>
    <body>
        <p>Уважаемый абонент!</p>
        <p>Вам предоставлен доступ к личному кабинету абонента.</p>
        <p>Ваши учетные данные:
			<br />
            Логин:  <b>{0}</b>
            <br />
            Пароль: <b>{1}</b>
        </p>
        <p>
            Ваша управляющая организация: ООО ""Жилищные услуги""; г. Владивосток, пр-т Острякова, 38; <br />
            Расчетный центр: 246-92-40, 261-95-84, Пн-Пт 8:00-17:00, обед 12-13, среда неприемный день <br />
            Главный офис: 246-46-01. Авар. служба 261-47-14, 298-09-81 <br />
            www.dom-vl.ru
        </p>
        <p>Чтобы открыть личный кабинет, перейдите по <a href=""http://www.qvitex.ru/"">ссылке</a>.</p>
		<strong>Это письмо создано автоматически и не требует ответа!</strong>
        <br />
        <p><a href=""http://www.qvitex.ru/"">Qvitex.ru</a> - сервис для управляющих организаций и их абонентов</p>
    </body>
</html>",
                        customerEmail, password);

                _mailMsg.AlternateViews.Add(
                    AlternateView.CreateAlternateViewFromString(_plainText, null, MediaTypeNames.Text.Plain));

                _mailMsg.AlternateViews.Add(
                    AlternateView.CreateAlternateViewFromString(_htmlText, null, MediaTypeNames.Text.Html));
    
                SmtpClient _smtpClient = new SmtpClient(_set.Server, _set.Port)
                {
                    Credentials = new System.Net.NetworkCredential(_set.Login, _set.Password), 
                    EnableSsl = true
                };

                _smtpClient.Send(_mailMsg);
            }
            catch (Exception _ex)
            {
                string _errMsg = string.Format("Не удалось отправить Email на {0}", customerEmail);
                Logger.SimpleWrite(_errMsg + " " + _ex);
                throw new ApplicationException(_errMsg, _ex);
            }
        }

        public void SendRegularBills(MemoryStream pdf, string customerEmail, string customerName, DateTime period)
        {
            SmtpSettings _set = SettingsSrv.GetSmtpSettings();

            using (MailMessage _mailMsg = new MailMessage())
            {

                _mailMsg.To.Add(new MailAddress(customerEmail, customerName));
                _mailMsg.From = new MailAddress(_set.SenderEmail, _set.SenderName);

                _mailMsg.Subject = string.Format("Квитанция за {0:MMMM yyyy}", period);

                string _plainText =
                    string.Format(
@"Уважаемый абонент!

Ваша квитанция за {0:MMMM yyyy} года находится в приложении к этому письму.
Если у Вас возникли вопросы, Вы можете обратиться абонентский отдел Вашей управляющей организации: 
ООО ""Жилищные услуги""; г. Владивосток, пр-т Острякова, 38;
Расчетный центр: 246-92-40, 261-95-84, Пн-Пт 8:00-17:00, обед 12-13, среда неприемный день
Главный офис: 246-46-01. Авар. служба 261-47-14, 298-09-81 
www.dom-vl.ru

Чтобы открыть личный кабинет, перейдите по ссылке: www.qvitex.ru

Это письмо создано автоматически и не требует ответа!

Qvitex.ru - сервис для управляющих организаций и их абонентов", 
                        period);

                string _htmlText =
                    string.Format(
@"<!DOCTYPE html>
<html>
    <body>
        <p>Уважаемый абонент!</p>
        <p>Ваша квитанция за {0:MMMM yyyy} года находится в приложении к этому письму.</p>
        <p>
            Если у Вас возникли вопросы, Вы можете обратиться абонентский отдел Вашей управляющей организации: <br />
            ООО ""Жилищные услуги""; г. Владивосток, пр-т Острякова, 38; <br />
            Расчетный центр: 246-92-40, 261-95-84, Пн-Пт 8:00-17:00, обед 12-13, среда неприемный день <br />
            Главный офис: 246-46-01. Авар. служба 261-47-14, 298-09-81 <br />
            www.dom-vl.ru
        </p>
        <p>Чтобы открыть личный кабинет, перейдите по <a href=""http://www.qvitex.ru/"">ссылке</a>.</p>
		<strong>Это письмо создано автоматически и не требует ответа!</strong>
        <br />
        <p><a href=""http://www.qvitex.ru/"">Qvitex.ru</a> - сервис для управляющих организаций и их абонентов</p>
    </body>
</html>",
                        period);

                _mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(_plainText, null, MediaTypeNames.Text.Plain));
                _mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(_htmlText, null, MediaTypeNames.Text.Html));

                ContentType _ct = new ContentType(MediaTypeNames.Application.Pdf);
                Attachment _attachment = new Attachment(pdf, _ct);
                _attachment.ContentDisposition.FileName = string.Format("bill_{0:MM-yyyy}.pdf", period);
                _mailMsg.Attachments.Add(_attachment);


                using (SmtpClient _smtpClient = new SmtpClient(_set.Server, _set.Port))
                {
                    _smtpClient.Credentials = new System.Net.NetworkCredential(_set.Login, _set.Password);
                    _smtpClient.EnableSsl = true;
                    _smtpClient.Send(_mailMsg);
                }
            }
        }
    }
}