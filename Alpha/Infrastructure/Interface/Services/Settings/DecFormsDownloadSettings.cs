namespace Taumis.Alpha.Infrastructure.Interface.Services.Settings
{
    public class DecFormsDownloadSettings
    {
        public DecFormsDownloadSettings(
            string server, int port, string login, string password, string sender)
        {
            Server = server;
            Port = port;
            Login = login;
            Password = password;
            Sender = sender;
        }

        public string Server { get; private set; }

        public int Port { get; private set; }

        public string Login { get; private set; }

        public string Password { get; private set; }

        public string Sender { get; private set; }
    }
}