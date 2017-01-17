using System;
using System.IO;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;

namespace Taumis.EnterpriseLibrary.Win.Services
{
    /// <summary>
    /// Сервис логгирования
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Порядковый номер лога
        /// </summary>
        static int _logId = 0;

        /// <summary>
        /// Имя папки для хранения логов
        /// </summary>
        const string LogDir = "Logs";

        /// <summary>
        /// Объект для потокобезопасной записи логов
        /// </summary>
        static object _locker = new object();

        /// <summary>
        /// Возвращает файл для записи логов
        /// </summary>
        /// <param name="_creationDateTime"></param>
        /// <returns></returns>
        private static StreamWriter GetFileToLog(DateTime _creationDateTime)
        {
            string _fullDirName =
                String.Format("{5}/{0}/{1}/{2:00}/{3:00}/{4:00}_00",
                    LogDir,
                    _creationDateTime.Year, _creationDateTime.Month, _creationDateTime.Day,
                    _creationDateTime.Hour,
                    System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName));

            // Если директория не существовала, обнуляем счетчик логов
            if (!Directory.Exists(_fullDirName))
            {
                _logId = 0;
            }

            DirectoryInfo _dirInfo = Directory.CreateDirectory(_fullDirName);

            string logFile = String.Format("{0}/log.txt", _fullDirName);

            return new StreamWriter(logFile, true);
        }

        /// <summary>
        /// Записывает строку в файл лога
        /// </summary>
        /// <param name="_strToLog"></param>
        public static void Write(string _strToLog)
        {
            // Блокируем текущий поток
            lock (_locker)
            {
                DateTime _now = ServerTimeServiceHolder.ServerTimeService.GetDateTimeInfo().Now;

                StreamWriter _stream = GetFileToLog(_now);
                _stream.WriteLine("№{0} {1:yyyy-MM-dd HH:mm:ss}\t{2}", ++_logId, _now, _strToLog);

                _stream.Flush();
                _stream.Close();
            }
        }

		/// <summary>
		/// Записывает строку в файл лога
		/// </summary>
		/// <param name="_strToLog"></param>
		public static void SimpleWrite(string _strToLog)
		{
			// Блокируем текущий поток
			lock (_locker)
			{
				DateTime _now = DateTime.Now;

				StreamWriter _stream = GetFileToLog(_now);
                _stream.WriteLine("№{0} {1:yyyy-MM-dd HH:mm:ss}\t{2}", ++_logId, _now, _strToLog);

				_stream.Flush();
				_stream.Close();
			}
		}
    }
}