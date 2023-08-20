using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using qwerty_chat_api.Models;
using qwerty_chat_api.Services;

namespace qwerty_chat_api.Controllers
{
    [Authorize]
    [Route("api/message")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessagesService _messageService;

        public MessagesController(MessagesService messagesService) 
        { 
            _messageService = messagesService;
        }

        [HttpGet]
        [Route("get-current-messages")]
        public async Task<List<Message>> GetCurrentMessages(string chat_id)
        {
            return await _messageService.GetMessagesByChatId(chat_id);
        }

        [HttpPost]
        [Route("create-new-message")]
        public async Task<Message> CreateNewMessage(string id, string message)
        {
            var mess = new Message()
            {
                Id = id,
                Text = message,
                CreatedDate = DateTime.UtcNow,
                UserId = id,
                User = new MongoDB.Driver.MongoDBRef("Users", id)
            };
            await _messageService.CreateMessage(mess);
            return mess;
        }

    }
}
