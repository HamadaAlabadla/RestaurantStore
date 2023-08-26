using Microsoft.AspNetCore.SignalR;

namespace RestaurantStore.Infrastructure.Hubs
{
	public class FileUploadHub : Hub
	{
		public override async Task OnConnectedAsync()
		{
			// Get the connectionId of the connected client
			var connectionId = Context.ConnectionId;
			string? userId = Context.UserIdentifier;
			if (userId != null)
				await Groups.AddToGroupAsync(connectionId, userId);

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
		public async Task SendProgress(string connectionId, int progress)
		{
			await Clients.Client(connectionId).SendAsync("ReceiveProgress", progress);
		}
	}
}
