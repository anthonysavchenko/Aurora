using System;
using Taumis.Alpha.Infrastructure.Interface.Commands;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands.DbBackup
{
    public class BackupDbCommand : ICommand
    {
        public Action OnSuccess { get; set; }
        public Action<string> OnFailedAction { get; set; }
        public Action<int> OnProgressChanged { get; set; }
        public string BackupPath { get; set; }
    }
}
