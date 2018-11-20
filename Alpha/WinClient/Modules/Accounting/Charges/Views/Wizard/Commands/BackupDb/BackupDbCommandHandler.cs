using System;
using System.ComponentModel;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands.DbBackup
{
    public class BackupDbCommandHandler : ICommandHandler<BackupDbCommand>
    {
        private readonly IServerTimeService _timeService;

        public BackupDbCommandHandler(IServerTimeService timeService)
        {
            _timeService = timeService;
        }

        public void Execute(BackupDbCommand command)
        {
            if (string.IsNullOrEmpty(command.BackupPath))
            {
                command.OnFailedAction("Укажите путь резервного копирования (меню \"Сервис\" > \"Настройки\")");
            }
            else
            {
                BackgroundWorker _backupWorker = new BackgroundWorker();
                _backupWorker.DoWork += (sender, args) =>
                {
                    string _path = (string)args.Argument;
                    BackgroundWorker _worker = (BackgroundWorker)sender;
                    args.Result = StartBackup(_path, _worker.ReportProgress);
                };

                _backupWorker.WorkerReportsProgress = true;
                _backupWorker.ProgressChanged += (sender, args) =>
                {
                    command.OnProgressChanged(args.ProgressPercentage);
                };

                _backupWorker.RunWorkerCompleted += (sender, args) =>
                {
                    bool _success = (bool)args.Result;

                    if (_success)
                    {
                        command.OnSuccess();
                    }
                    else
                    {
                        command.OnFailedAction("При создании резервной копии возникла ошибка");
                    }
                };

                _backupWorker.RunWorkerAsync(command.BackupPath);
            }
        }

        /// <summary>
        /// Запускает процесс резервного копирования базы данных
        /// </summary>
        /// <param name="backupPath">Путь резервного копирования</param>
        /// <param name="progressAction">Метод, вызываемый при изменении прогресса резервного копирования</param>
        /// <returns>Признак успешного завершения процесса</returns>
        private bool StartBackup(string backupPath, Action<int> progressAction)
        {
            bool _result;

            DateTime _now = _timeService.GetDateTimeInfo().Now;

            using (var _db = new Entities())
            {
                try
                {
                    int _sqlErrors = 0;

                    var _entityConnection = (EntityConnection)_db.Connection;
                    var _sqlConnection = (SqlConnection)_entityConnection.StoreConnection;
                    _sqlConnection.FireInfoMessageEventOnUserErrors = true;
                    _sqlConnection.InfoMessage += (sender, args) =>
                    {
                        if (!ProcessSqlConnectionInfoMessage(args.Errors, progressAction))
                        {
                            _sqlErrors++;
                        }
                    };

                    string _fileName = $"{_sqlConnection.Database}_{_now:yyyy-MM-dd_HH-mm-ss}.bak";

                    _db.CommandTimeout = 3600;
                    _db.ContextOptions.EnsureTransactionsForFunctionsAndCommands = false;
                    _db.BackupDatabase(_sqlConnection.Database, Path.Combine(backupPath, _fileName), 5);

                    _result = _sqlErrors == 0;
                }
                catch (Exception ex)
                {
                    Logger.Write($"WizardViewPresenter.StartBackup(): {ex}");
                    _result = false;
                }
            }

            return _result;
        }

        /// <summary>
        /// Обрабатывет информационные сообщения, поступающие от SQL-сервера 
        /// </summary>
        /// <param name="errors">Коллекция сообщений (ошибок)</param>
        /// <param name="progressAction">Метод, вызываемый при изменении прогресса резервного копирования</param>
        /// <returns>true - информационное сообщение, false - сообщение об ошибке</returns>
        private bool ProcessSqlConnectionInfoMessage(SqlErrorCollection errors, Action<int> progressAction)
        {
            if (errors.Count == 0)
            {
                return true;
            }

            bool _result = true;
            var _error = errors[0];
            Regex _regex = new Regex("^\\d{1,3}", RegexOptions.Compiled);

            if (_error.Class == 0)
            {
                if (_regex.IsMatch(_error.Message))
                {
                    int _progress = int.Parse(_regex.Match(_error.Message).Value);
                    progressAction(_progress);
                }
            }
            else
            {
                string _msg = "DbBackupCommandHandler.ProcessSqlConnectionInfoMessage(): {0}, line {1}: {2}";
                Logger.Write(string.Format(_msg, _error.Procedure, _error.LineNumber, _error.Message));
                _result = false;
            }

            return _result;
        }
    }
}
