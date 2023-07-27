using System;
using Microsoft.AspNetCore.SignalR;
using DemoSignalrChatServerApplication.Data;
using DemoSignalrChatServerApplication.Models;

namespace DemoSignalrChatServerApplication.Hubs
{
    public class ChatHub : Hub
    {

        public async Task GetNickName(string nickName)
        {
            Client client = new Client
            {
                ConnectionId = Context.ConnectionId,
                NickName = nickName
            };
            ClientSource.Clients.Add(client);
            await Clients.Others.SendAsync("clientJoined", nickName);
            await Clients.All.SendAsync("clients", ClientSource.Clients);
        }

        public async Task SendMessageAsync(string message, string clientName)
        {
            var senderClient = ClientSource.Clients.FirstOrDefault(client => client.ConnectionId == Context.ConnectionId);
            clientName = clientName.Trim();
            // Console.WriteLine(clientName);
            // Console.WriteLine(senderClient.NickName);
            // Console.WriteLine(senderClient.ConnectionId);

            if (clientName == "All")
            {
                await Clients.Others.SendAsync("receiveMessage", message, senderClient.NickName);
            }
            else
            {	
                Client client = ClientSource.Clients.FirstOrDefault(c => c.NickName == clientName);
                await Clients.Client(client.ConnectionId).SendAsync("receiveMessage", message, senderClient.NickName);
            }
        }
    }

}
