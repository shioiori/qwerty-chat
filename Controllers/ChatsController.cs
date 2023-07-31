using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace qwerty_chat_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly ChatsController _chatsController;

        public ChatsController(ChatsController chatsController) 
        {
            _chatsController = chatsController;
        }

        /*[HttpGet]
        public async Task<> GetChat()
        {
            var user = Http
        } */
    }
}
