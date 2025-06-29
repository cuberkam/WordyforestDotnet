using EntityLayer.Entities;

namespace WordyforestDotnet.BusinessLayer.Services.Abstract
{
    public interface ISubscribedListService
    {
        Task<List<VocabulariesList>> GetUserSubscribedLists(string userId);
        Task<VocabulariesList?> CreateSubscribedList(string userId, string shareCode);
        Task DeleteSubscribedList(string userId, int vocabularyListId);
    }
}
