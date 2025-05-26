using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WordyforestDotnet.Data;
using WordyforestDotnet.Models;
using WordyforestDotnet.Services;

namespace WordyforestDotnet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly VocabularyService _vocabularyService;
        private readonly VocabulariesListService _vocabulariesListService;

        public HomeController(
            ILogger<HomeController> logger, 
            ApplicationDbContext context, 
            VocabularyService vocabularyService, 
            VocabulariesListService vocabulariesListService
            )
        {
            _logger = logger;
            _context = context;
            _vocabularyService = vocabularyService;
            _vocabulariesListService = vocabulariesListService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.publicLists = await _vocabulariesListService.GetPublicVocabulariesLists();

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                ViewBag.userLists = await _vocabulariesListService.GetUserVocabulariesLists(userId!);
                ViewBag.subscribetedLists = await _vocabulariesListService.GetUserSubscribedLists(userId!);

            }
            return View( await _vocabularyService.GetRandomVocabularyByVocabulariesList());
        }

        public async Task<IActionResult> NextVocabulary(int id)
        {
            return PartialView("_NextVocabulary", await _vocabularyService.GetRandomVocabularyByVocabulariesList(id));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
