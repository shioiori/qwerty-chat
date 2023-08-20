using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using qwerty_chat_api.Models;
using qwerty_chat_api.Services;
using System.Security.Claims;

namespace qwerty_chat_api.Controllers
{
    [Authorize]
    [Route("api/chat")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly ChatsService _chatsService;
        private readonly MessagesService _messagesService;
        private readonly UsersService _usersService;

        public ChatsController(ChatsService chatsService, UsersService usersService, MessagesService messagesService) 
        {
            _chatsService = chatsService;
            _messagesService = messagesService;
            _usersService = usersService;
        }

        [HttpGet]
        [Route("get-current-connection")]
        public async Task<List<Chat>> GetCurentConnection()
        {
            try
            {
                var user_id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var chats = await _chatsService.GetUserChat(user_id);
                return chats;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("get-current-chat")]
        public async Task<Chat> GetCurrentChat(string id)
        {
            var chat = await _chatsService.GetChatById(id);
            if (chat == null)
            {
                var user_id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                CreateNewChat(new string[] { user_id, id }, null);
            }
            return chat;
        }


        [HttpPost]
        [Route("create-new-chat")]
        public async Task<Chat> CreateNewChat(string[] member_ids, string? name, bool limit = true)
        {
            try
            {
                var chat = new Chat()
                {
                    Name = name,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    //CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                    IsStored = false,
                    IsLimited = limit,
                    MemberIds = member_ids,
                    Members = await Task.WhenAll(member_ids.Select(async x => await _usersService.GetUserAsync(x))),
                };
                await _chatsService.CreateAsync(chat);
                return chat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPut]
        public async Task<IActionResult> StoreChat(string id)
        {
            try
            {
                var chat = await _chatsService.GetAsync(id);
                chat.IsStored = true;
                await _chatsService.UpdateAsync(id, chat);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
