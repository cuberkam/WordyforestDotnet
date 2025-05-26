using Microsoft.AspNetCore.Mvc;
using WordyforestDotnet.Data;
using WordyforestDotnet.Models.DTOs;
using WordyforestDotnet.Services;

namespace WordyforestDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocabulariesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly VocabularyService _vocabularyService;

        public VocabulariesController(ApplicationDbContext context, VocabularyService vocabularyService)
        {
            _context = context;
            _vocabularyService = vocabularyService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string word, string vocabulariesListId)
        {
            int excludedListId = Convert.ToInt32(vocabulariesListId);
            var result =await _vocabularyService.SearchVocabulariesByVocabulariesList(word, excludedListId);

            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] VocabulariesRequest request)
        {
            var addVocabularyToVocabulariesList = await _vocabularyService.AddVocabularyToVocabulariesList(request.VocabulariesListId, request.VocabularyId);
            if (addVocabularyToVocabulariesList == null) return NotFound();

            return Ok(addVocabularyToVocabulariesList);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] VocabulariesRequest request)
        {
            var removedVocabulary = await _vocabularyService.RemoveVocabularyFromVocabulariesList(request.VocabulariesListId, request.VocabularyId);
            if (removedVocabulary == false) return NotFound();

            return Ok();
        }
    }
}
