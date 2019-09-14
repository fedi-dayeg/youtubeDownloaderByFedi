using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;
using YoutubeExplode.Models;
using NReco.VideoConverter;

namespace youtubeDownloaderByFedi
{
    class DownloadYoutube
    {
        private LoadingControl loadingControl = ((MainWindow)System.Windows.Application.Current.MainWindow).LoadingControl;
        private YoutubeClient client;
        private BackgroundWorker backgroundWorker = new BackgroundWorker();
        private BackgroundWorker worker = new BackgroundWorker();
        private string url;
        private string path;

        public DownloadYoutube(String url, String path)
        {
            this.url = url;
            this.path = path;
        }

        public void RunDownloder()
        {
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);

            worker.DoWork += new DoWorkEventHandler( worker_DoWorkAsync);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();

        }

         void worker_ProgressChanged(object sender , ProgressChangedEventArgs e)
        {
            loadingControl.loadingTextBlock.Text = e.ProgressPercentage.ToString() + " %";
        }

        private void worker_RunWorkerCompleted(object sender , RunWorkerCompletedEventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }

        private async void worker_DoWorkAsync(object sender, DoWorkEventArgs e)
        {
            loadingControl.Dispatcher.Invoke(new Action(() =>
            {
                loadingControl.LoadingStateText.Text = "Get Downloading Urls ... ";
            }));

            loadingControl.Dispatcher.Invoke(new Action(() =>
           {
               loadingControl.loadingTextBlock.Text = " 0%";
           }));

            string link = url;
            var id = YoutubeClient.ParseVideoId(url);
            client = new YoutubeClient();
            var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(id);
            
                var streamInfo = streamInfoSet.Video
                    .Where(s => s.Container == YoutubeExplode.Models.MediaStreams.Container.Mp4)
                    .OrderByDescending(s => s.VideoQuality)
                    .ThenByDescending(s => s.Framerate)
                    .First();

            var ext = streamInfo.Container.GetFileExtension();

            await client.DownloadMediaStreamAsync(streamInfo, $"downloaded_video.{ext}");
            worker.ReportProgress(100);
        }


        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            loadingControl.Dispatcher.Invoke(new Action(() =>
            {
                loadingControl.LoadingStateText.Text = "Downloading MP3 File ... ";
            }
            ));
        }

        //private void downloaderProgressChange(object sender,  )

        private void backgroundWorker_ProgressChanged(object sender , ProgressChangedEventArgs e)
        {
            loadingControl.loadingTextBlock.Text = e.ProgressPercentage.ToString() + " %";
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Grid loadingGrid = ((MainWindow)System.Windows.Application.Current.MainWindow).Loading;
            Grid completedGrid = ((MainWindow)System.Windows.Application.Current.MainWindow).Done;

            loadingGrid.Visibility = Visibility.Hidden;
            completedGrid.Visibility = Visibility.Visible;
        }


    }
}
