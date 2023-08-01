using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RestaurantStore.Infrastructure.Hubs
{
	public class NotificationHub : Hub
	{
		public override async Task OnConnectedAsync()
		{
			// Get the user identifier (e.g., user ID) and add the connection to a group
			string? userId = Context.UserIdentifier;
			if (userId != null)
				await Groups.AddToGroupAsync(Context.ConnectionId, userId);

			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			// Get the user identifier (e.g., user ID) and remove the connection from the group
			string? userId = Context.UserIdentifier;
			if (userId != null)
				await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);

			await base.OnDisconnectedAsync(exception);
		}
		public async Task SendNotification(string user, string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}
	}
}
