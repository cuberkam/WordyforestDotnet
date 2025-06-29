using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WordyforestDotnet.BusinessLayer.Services.Abstract;
using WordyforestDotnet.EntityLayer.DTOs;

namespace WordyforestDotnet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribedListController : ControllerBase
    {
        private readonly ISubscribedListService _subscribedListService;
        private readonly IAuthService _authService;


        public SubscribedListController(ISubscribedListService subscribedListService, IAuthService authService)
        {
            _subscribedListService = subscribedListService;
            _authService = authService;
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetUserSubscribedLists()
        {
            var userId = _authService.GetCurrentUserId();
            return Ok(await _subscribedListService.GetUserSubscribedLists(userId!));
        }

        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> CreateSubscribedList([FromBody] SubscribedListRequest request)
        {
            var userId = _authService.GetCurrentUserId();
            var result = await _subscribedListService.CreateSubscribedList(userId!, request.ShareId);
            if(result == null) return BadRequest("Not Found");

            var response = new SubscribedListResponse { Id = result.Id, Name = result.Name };

            return Ok(response);
        }

        [Authorize]
        [HttpDelete()]
        public async Task<IActionResult> DeleteSubscribedList([FromBody] SubscribedListDeleteRequest request)
        {
            var userId = _authService.GetCurrentUserId();
            await _subscribedListService.DeleteSubscribedList(userId!, request.VocabulariesListId);
            return NoContent();
        }
    }
}
