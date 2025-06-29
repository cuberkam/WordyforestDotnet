using EntityLayer.Entities;
using WordyforestDotnet.BusinessLayer.Services.Abstract;
using WordyforestDotnet.DataAccessLayer.Repositories.Abstract;

namespace WordyforestDotnet.BusinessLayer.Services.Concrete
{
    public class VocabulariesListManager : IVocabulariesListService
    {
        private readonly IVocabularyRepository _vocabularyRepository;
        private readonly IVocabulariesListRepository _vocabulariesListRepository;

        public VocabulariesListManager(IVocabularyRepository vocabularyRepository, IVocabulariesListRepository vocabulariesListRepository)
        {
            _vocabularyRepository = vocabularyRepository;
            _vocabulariesListRepository = vocabulariesListRepository;
        }

        public async Task<VocabulariesList?> GetVocabulariesList(int vocabulariesId)
        {
            return await _vocabulariesListRepository.GetByIdWithVocabularies(vocabulariesId);
        }

        public async Task<VocabulariesList?> GetVocabulariesListByShareCode(string shareCode)
        {
            return await _vocabulariesListRepository.GetByShareCode(shareCode);
        }

        public async Task<VocabulariesList?> CreateVocabulariesList(string vocabulariesListName, string userdId)
        {
            return await _vocabulariesListRepository.Create(vocabulariesListName, userdId);
        }

        public async Task<bool> DeleteVocabulariesList(int vocabulariesListId, string userId)
        {
            VocabulariesList? vocabularyList = await _vocabulariesListRepository.GetByIdAndByUserId(vocabulariesListId, userId);

            return await _vocabulariesListRepository.Delete(vocabularyList);
        }

        public async Task<List<VocabulariesList>> GetPublicVocabulariesLists()
        {
            return await _vocabulariesListRepository.GetPublicLists();
        }

        public async Task<List<VocabulariesList>> GetUserVocabulariesLists(string userId)
        {
            return await _vocabulariesListRepository.GetUserLists(userId);
        }
    }
}
