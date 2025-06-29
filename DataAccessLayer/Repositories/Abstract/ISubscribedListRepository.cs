using EntityLayer.Entities;

namespace WordyforestDotnet.DataAccessLayer.Repositories.Abstract
{
    public interface ISubscribedListRepository
    {
        Task<SubscribedList?> GetByVocabulariesListIdAndUserId(string userId, int vocabulariesListId);
        Task<List<VocabulariesList>> GetUserSubscribedLists(string userId);
        Task<VocabulariesList?> Create(string userId, VocabulariesList vocabulariesList);
        Task Delete(SubscribedList? subscribedList);
    }
}
