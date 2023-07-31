using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using qwerty_chat_api.Models;
using qwerty_chat_api.Utilities;
using qwerty_chat_api.ViewModels;
using System.Data;

namespace qwerty_chat_api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly DataContext _dataContext;

        public ChatHub(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task SendAll(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendSpecifiedUser(string sender, string receiver, string message)
        {
            var _sender = Cryptography.Decrypt(sender);
            var _receiver = Cryptography.Decrypt(receiver);
            var user = _dataContext.Users.Where(x => x.UserId == Guid.Parse(_receiver)).FirstOrDefault();
            var vm = new UserVM()
            {
                UserId = receiver,
                Name = user.Name,
                Avatar = user.Avatar,
                UpdatedDate = DateTime.Now,
            };
            var info = JsonConvert.SerializeObject(vm);
            SqlParameter[] @params = {
                new SqlParameter("@user_id1", _sender),
                new SqlParameter("@user_id2", _receiver),
                new SqlParameter("@chat_id", SqlDbType.UniqueIdentifier) { Direction = ParameterDirection.Output }
            };
            var res = _dataContext.ChatUsers.FromSqlRaw($"exec FindPrivateChatId @user_id1, @user_id2, @chat_id OUTPUT", @params);
            Guid chatId = Guid.NewGuid();
            if (Guid.TryParse(@params[2].Value?.ToString(), out Guid x))
            {
                chatId = Guid.Parse(@params[2].Value.ToString());
            }
            else
            {
                _dataContext.Chats.Add(new Chat()
                {
                    ChatId = chatId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                });
                await _dataContext.SaveChangesAsync();
            }
            _dataContext.Messages.Add(new Message()
            {
                MessageId = Guid.NewGuid(),
                MessageContent = message,
                UserId = Guid.Parse(_receiver),
                ChatId = chatId,
                CreatedDate = DateTime.Now,
            });
            await _dataContext.SaveChangesAsync();
            await Clients.User(user.Email).SendAsync("ReceiveMessage", info, message);
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
