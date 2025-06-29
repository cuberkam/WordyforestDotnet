using EntityLayer.Entities;
using WordyforestDotnet.BusinessLayer.Services.Abstract;
using WordyforestDotnet.DataAccessLayer.Repositories.Abstract;

namespace WordyforestDotnet.BusinessLayer.Services.Concrete
{
    public class SubscribedListManager : ISubscribedListService
    {
        private readonly IVocabulariesListRepository _vocabulariesListRepository;
        private readonly ISubscribedListRepository _subscribedListRepository;

        public SubscribedListManager(IVocabulariesListRepository vocabulariesListRepository, ISubscribedListRepository subscribedListRepository)
        {
            _vocabulariesListRepository = vocabulariesListRepository;
            _subscribedListRepository = subscribedListRepository;
        }

        public async Task<List<VocabulariesList>> GetUserSubscribedLists(string userId)
        {
            return await _subscribedListRepository.GetUserSubscribedLists(userId);
        }

        public async Task<VocabulariesList?> CreateSubscribedList(string userId, string shareCode)
        {
            var vocabulariesList = await _vocabulariesListRepository.GetByShareCode(shareCode);
            if (vocabulariesList == null || vocabulariesList.ExtendedUserId == userId) return null;

            return await _subscribedListRepository.Create(userId, vocabulariesList);
        }

        public async Task DeleteSubscribedList(string userId, int vocabularyListId)
        {
            SubscribedList? subscribedList = await _subscribedListRepository.GetByVocabulariesListIdAndUserId(userId, vocabularyListId);

            await _subscribedListRepository.Delete(subscribedList);
        }
    }
}
