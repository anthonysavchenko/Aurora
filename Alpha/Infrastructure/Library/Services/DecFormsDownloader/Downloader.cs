﻿using System;
using System.ComponentModel;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.DecFormsDownloader.DecFormsImapDownloader;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsDownloader
{
    static public class Downloader
    {
        static public void DownloadAsync(
            string directory,
            int userID,
            string note,
            Action<int, string> OnProgress,
            Action<DecFormsDownloads> OnCompleted)
        {
            BackgroundWorker worker = new BackgroundWorker()
            {
                WorkerReportsProgress = true
            };

            worker.ProgressChanged += (sender, args) =>
            {
                OnProgress(args.ProgressPercentage, (string)args.UserState);
            };

            worker.RunWorkerCompleted += (sender, args) =>
            {
                OnCompleted((DecFormsDownloads)args.Result);
            };

            worker.DoWork += (sender, args) =>
            {
                args.Result =
                    Download(
                        directory,
                        userID,
                        note,
                        ((BackgroundWorker)sender).ReportProgress);
            };

            worker.RunWorkerAsync();
        }

        static private DecFormsDownloads Download(
            string directory,
            int userID,
            string note,
            Action<int, string> SetProgress)
        {
            SetProgress(0, "Подготовка к началу обработки данных...");

            if (DownloadHandler.CreateDownload(
                directory,
                note,
                userID,
                out DecFormsDownloads download))
            {
                try
                {
                    if (!ImapDownloader.Download(
                        download,
                        0,
                        100,
                        SetProgress))
                    {
                        return download;
                    }
                }
                catch (Exception e)
                {
                    Logger.SimpleWrite($"Downloader Download error: {e}");
                    DownloadHandler.UpdateDownloadWithError(
                        download,
                        "Ошибка во время обработки данных.",
                        e.ToString());
                }
            }

            return download;
        }
    }
}