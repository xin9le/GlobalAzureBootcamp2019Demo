using System;
using System.Text;
using System.Net.Http;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;



namespace Xtuber.Wpf
{
    public partial class MainWindow : Window
    {
        private const string RootUrl = "http://localhost:7071";


        private HttpClient HttpClient { get; }
        private HubConnection Connection { get; }


        public MainWindow()
        {
            this.InitializeComponent();
            this.HttpClient = new HttpClient();
            this.Connection = new HubConnectionBuilder().WithUrl($"{RootUrl}/api").Build();

            this.Connection.On<string>("Receive", data =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.StatusText.Content = data;
                });
            });
        }


        private async void OnConnectClick(object sender, RoutedEventArgs e)
        {
            try
            {
                await this.Connection.StartAsync();
                this.StatusText.Content = "Connected!";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private async void OnDisconnectClick(object sender, RoutedEventArgs e)
        {
            await this.Connection.StopAsync();
            this.StatusText.Content = "Disconnected!";
        }


        private async void OnBroadcastClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var url = $"{RootUrl}/api/broadcast";
                var now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
                using var content = new StringContent(now, Encoding.UTF8);
                var response = await this.HttpClient.PostAsync(url, content);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
