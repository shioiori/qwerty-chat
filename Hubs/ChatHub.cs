using Microsoft.AspNetCore.SignalR;
using qwerty_chat_api.Models;
using qwerty_chat_api.Services.Interface;
using System.Data;
using System.Security.Claims;

namespace qwerty_chat_api.Hubs
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, string> UserConnections = new Dictionary<string, string>();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMessage _messageService;

        public ChatHub(IHttpContextAccessor httpContextAccessor, IMessage messageService)
        {
            _httpContextAccessor = httpContextAccessor;
            _messageService = messageService;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }


        public async Task SendAll(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendSpecifiedUser(string sender, string receiver, string message)
        {
            /*var user_id = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _messageService.CreateMessage(new Message()
            {
                Text = message,
                CreatedDate = DateTime.UtcNow,
                UserId = user_id,
            });
            await Clients.User(user_id).SendAsync("ReceiveMessage", );*/
        }

        public async Task SendGroup(string groupName, string username, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", username, message);
        }

        public async Task AddToGroup(string groupName, string username)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("AddToGroup", $"{username} has joined the group {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName, string username)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("RemoveFromGroup", $"{username} has left the group {groupName}.");
        }

        public async Task OnConnectedAsync(string username)
        {
            await Clients.User(username).SendAsync("OnConnected", this.Context.ConnectionId);
        }
    }
}
