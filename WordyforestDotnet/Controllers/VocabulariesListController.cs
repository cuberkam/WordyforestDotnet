using WordyforestDotnet.EntityLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WordyforestDotnet.BusinessLayer.Services.Abstract;
using EntityLayer.Entities;

namespace WordyforestDotnet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocabulariesListController : ControllerBase
    {
        private readonly IVocabulariesListService _vocabulariesListService;
        private readonly IAuthService _authService;

        public VocabulariesListController(IVocabulariesListService vocabulariesListService, IAuthService authService)
        {
            _vocabulariesListService = vocabulariesListService;
            _authService = authService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVocabulariesList(int id)
        {
            var result = await _vocabulariesListService.GetVocabulariesList(id);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("sharecode/{shareCode}")]
        public async Task<IActionResult> GetVocabulariesListByShareCode(string shareCode)
        {
            var result = await _vocabulariesListService.GetVocabulariesListByShareCode(shareCode);
            return Ok(result);
        }

        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> CreateVocabulariesList([FromBody] VocabulariesListRequest request )
        {
            var vocabulariesListName = request.Name;
            if (vocabulariesListName=="" || vocabulariesListName==null) return BadRequest(new { message = "Vocabularies list name is empty." });

            var userId = _authService.GetCurrentUserId();

            var result = await _vocabulariesListService.CreateVocabulariesList(vocabulariesListName, userId!);
            if (result==null) return BadRequest(new { message = "User not found" });

            var response = new VocabulariesListResponse {
                Id=result.Id,
                Name=result.Name,
                ShareId=result.ShareId.ToString()
            };

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVocabulariesList(int id)
        {
            var userId = _authService.GetCurrentUserId();

            var result = await _vocabulariesListService.DeleteVocabulariesList(id, userId!);
            if (!result) return BadRequest(new { message = "Not found" });

            return NoContent();
        }

        [HttpGet("public")]
        public async Task<IActionResult> GetPublicVocabulariesLists()
        {
            return Ok(await _vocabulariesListService.GetPublicVocabulariesLists());
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetUserVocabulariesLists()
        {
            var userId = _authService.GetCurrentUserId();
            return Ok( await _vocabulariesListService.GetUserVocabulariesLists(userId!));
        }
    }
}
