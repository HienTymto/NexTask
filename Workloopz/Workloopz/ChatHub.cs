using Microsoft.AspNetCore.SignalR;

namespace Workloopz
{
	public class ChatHub : Hub
	{
		//public async Task SendMessage(string user, string message)
		//{
		//	await Clients.All.SendAsync("ReceiveMessage", user, message);
		//}
		public async Task SendMessage(string user, string message, string taskId)
		{
			await Clients.All.SendAsync("ReceiveMessage", user, message, taskId);
		}

		// Gửi bình luận mới cho các client trong cùng một task
		public async Task SendTaskComment(string user, string message, string taskId)
		{
			// Group client theo taskId
			await Clients.Group(taskId).SendAsync("ReceiveTaskComment", user, message, taskId);
		}
	}
}
