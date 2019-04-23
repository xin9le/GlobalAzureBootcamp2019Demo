using Microsoft.AspNetCore.SignalR.Client;
using System;
using UnityEngine;



namespace Xtuber.Unity
{
    public class SignalRBehaviour : MonoBehaviour
    {
        private const string RootUrl = "http://xtuber-api.azurewebsites.net";
        private HubConnection Connection { get; set; }


        private async void Start()
        {
            this.Connection
                = new HubConnectionBuilder()
                .WithUrl($"{RootUrl}/api")
                .Build();

            this.Connection.On<string>("Receive", data =>
            {
                Debug.Log(data);
            });

            try
            {
                await this.Connection.StartAsync();
                Debug.Log("Connected!");
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
    }
}
