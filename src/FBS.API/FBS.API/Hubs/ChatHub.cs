using FBS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FBS.API.Hubs
{
    [Authorize]
    public class ChatHub(IChatAuthorizationService chatService) : Hub
    {
        private readonly IChatAuthorizationService _chatService = chatService;
        public async Task SendMessage(string user, string message)
        {
            var userRole = _chatService.GetUserRole(Context.User);

            if (!_chatService.CanSendMessage(userRole))
            {
                await Clients.Caller.SendAsync("Error", _chatService.GetErrorMessage("send"));
                return;
            }

            await Clients.Others.SendAsync("ReceiveMessage", user,message);
        }
        public async Task JoinRoom(string RoomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, RoomName);
        }
        public async Task SendMessageToRoom(string room, string message,string user)
        {
            await Clients.Group(room).SendAsync("ReceiveMessage",user, message);
        }
        public override async Task OnConnectedAsync()
        {
            var user = Context.User?.Identity?.Name ?? "Unknown"; 
            await Clients.All.SendAsync("UserConnected", $"{user} подключился к чату");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = Context.User?.Identity?.Name ?? "Unknown";
            await Clients.All.SendAsync("UserDisconnected", $"{user} отключился от чата");
            await base.OnDisconnectedAsync(exception);
        } 

    }
}
