using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WordyforestDotnet.Data;
using WordyforestDotnet.Models.Entities;
using WordyforestDotnet.Services;

namespace WordyforestDotnet.Controllers
{
    [Authorize]
    public class VocabulariesListsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly VocabulariesListService _vocabulariesListService;

        public VocabulariesListsController(ApplicationDbContext context, VocabulariesListService vocabulariesListService)
        {
            _context = context;
            _vocabulariesListService = vocabulariesListService;
        }
        public async Task<IActionResult> MyLists()
        {
            ViewBag.userLists = await _vocabulariesListService.GetUserVocabulariesLists(GetUsedId());
            ViewBag.subscripedLists = await _vocabulariesListService.GetUserSubscribedLists(GetUsedId());

            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _vocabulariesListService.GetVocabulariesList(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedDate,IsPrivate,ShareId,ExtendedUserId")] VocabulariesList vocabulariesList)
        {
            await _vocabulariesListService.CreateVocabulariesList(vocabulariesList, GetUsedId());
            return RedirectToAction(nameof(MyLists));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(string shareCode)
        {
            string userId = GetUsedId();
            VocabulariesList? vocabularyList = await _vocabulariesListService.GetVocabulariesListByShareCode(shareCode);

            if (vocabularyList == null)
            {
                Console.WriteLine("Vocabulary list doesn't exists.");
                TempData["ErrorMessage"] = "Vocabulary list doesn't exists.";
                return RedirectToAction(nameof(MyLists));
            }

            if (vocabularyList != null && userId == vocabularyList.ExtendedUserId.ToString())
            {
                Console.WriteLine("User can't subscribe own vocabulary list.");
                TempData["ErrorMessage"] = "You can't subscribe own vocabulary list.";
                return RedirectToAction(nameof(MyLists));
            }

            bool isCreated = await _vocabulariesListService.CreateSubscribedList(userId, vocabularyList!);
            if (!isCreated) TempData["ErrorMessage"] = "Already subscribed this vocabularies list.";

            return RedirectToAction(nameof(MyLists));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unsubscribe(string shareCode)
        {
            await _vocabulariesListService.DeleteSubscribedList(shareCode, GetUsedId());
            return RedirectToAction(nameof(MyLists));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int vocabularyListId)
        {
            await _vocabulariesListService.DeleteVocabulariesList(vocabularyListId, GetUsedId());
            return RedirectToAction(nameof(MyLists));
        }

        [NonAction]
        private string GetUsedId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
    }
}
