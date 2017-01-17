namespace Taumis.Alpha.Server.Core.Services.Settings
{
    public class SmtpSettings
    {
        public SmtpSettings(
            string server, int port, string login, string password, string senderName, string senderEmail)
        {
            Server = server;
            Port = port;
            Login = login;
            Password = password;
            SenderName = senderName;
            SenderEmail = senderEmail;
        }

        public string Server { get; private set; }
        public int Port { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string SenderName { get; private set; }
        public string SenderEmail { get; private set; }
    }
}