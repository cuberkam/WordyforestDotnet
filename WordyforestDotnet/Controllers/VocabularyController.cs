using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WordyforestDotnet.BusinessLayer.Services.Abstract;
using WordyforestDotnet.EntityLayer.DTOs;

namespace WordyforestDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocabularyController : ControllerBase
    {
        private readonly IVocabularyService _vocabularyService;

        public VocabularyController(IVocabularyService vocabularyService)
        {
            _vocabularyService = vocabularyService;
        }

        [HttpGet("random")]
        public async Task<IActionResult> Random(int vlId)
        {
            var result = await _vocabularyService.GetRandomVocabularyByVocabulariesList(vlId);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> Search(string word, string vlId)
        {
            int excludedListId = Convert.ToInt32(vlId);
            var result = await _vocabularyService.SearchWordExcludeVocabulariesList(word, excludedListId);

            return Ok(result);
        }

        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] VocabulariesRequest request)
        {
            var addVocabularyToVocabulariesList = await _vocabularyService.AddVocabularyToVocabulariesList(request.VocabulariesListId, request.VocabularyId);
            if (addVocabularyToVocabulariesList == null) return NotFound();

            var response = new VocabularyResponse 
            {
                Id=addVocabularyToVocabulariesList.Id,
                Word=addVocabularyToVocabulariesList.Word,
                Description=addVocabularyToVocabulariesList.Description!
            };

            return Ok(response);
        }

        [Authorize]
        [HttpDelete()]
        public async Task<IActionResult> Remove([FromBody] VocabulariesRequest request)
        {
            var removedVocabulary = await _vocabularyService.RemoveVocabularyFromVocabulariesList(request.VocabulariesListId, request.VocabularyId);
            if (removedVocabulary == false) return NotFound();

            return NoContent();
        }
    }
}
