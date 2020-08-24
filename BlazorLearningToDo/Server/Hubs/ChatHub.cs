using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using BlazorLearningToDo.Shared;

namespace BlazorLearningToDo.Server.Hubs
{
	public class ChatHub : Hub
	{
		private static List<ChatMessage> trackedMessages = new List<ChatMessage>();

		public async Task SendMessage(string user, string message)
		{
			trackedMessages.Add(new ChatMessage
			{
				User = user,
				Content = message
			});

			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}

		public async Task FetchState()
		{
			await Clients.All.SendAsync("ReceiveServerState", trackedMessages.ToArray());
		}
	}
}
