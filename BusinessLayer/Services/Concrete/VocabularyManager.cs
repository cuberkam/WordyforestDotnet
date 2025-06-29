using EntityLayer.Entities;
using WordyforestDotnet.BusinessLayer.Services.Abstract;
using WordyforestDotnet.DataAccessLayer.Repositories.Abstract;

namespace WordyforestDotnet.BusinessLayer.Services.Concrete
{
    public class VocabularyManager : IVocabularyService
    {
        private readonly IVocabularyRepository _vocabularyRepository;
        private readonly IVocabulariesListRepository _vocabulariesListRepository;
        public VocabularyManager(IVocabularyRepository vocabularyRepository, IVocabulariesListRepository vocabulariesListRepository)
        {
            _vocabularyRepository = vocabularyRepository;
            _vocabulariesListRepository = vocabulariesListRepository;
        }

        public async Task<Vocabulary?> GetRandomVocabularyByVocabulariesList(int vocabulariesListId)
        {
            if (vocabulariesListId == 0)
            {
                return await _vocabularyRepository.GetRandomAsync();
            }

            return await _vocabularyRepository.GetRandomByListIdAsync(vocabulariesListId);
        }

        public async Task<List<Vocabulary>> SearchWordExcludeVocabulariesList(string word, int excludedListId)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                return new List<Vocabulary>();
            }
            var excludedVocabularyIds = await _vocabulariesListRepository.GetVocabulariesId(excludedListId);

            return await _vocabularyRepository.SearchWordAsync(word, excludedVocabularyIds);
        }

        public async Task<Vocabulary?> AddVocabularyToVocabulariesList(int vocabulariesListId, int vocabularyId)
        {
            var vocabulariesList = await _vocabulariesListRepository.GetByIdWithVocabularies(vocabulariesListId);
            if (vocabulariesList == null) return null;

            var vocabulary = await _vocabularyRepository.GetByIdAsync(vocabularyId);
            if (vocabulary == null) return null;

            await _vocabularyRepository.AddToVocabulariesListAsync(vocabulary, vocabulariesList);

            return vocabulary;
        }

        public async Task<bool> RemoveVocabularyFromVocabulariesList(int vocabulariesListId, int vocabularyId)
        {
            var vocabulariesList = await _vocabulariesListRepository.GetByIdWithVocabularies(vocabulariesListId);
            if (vocabulariesList == null) return false;

            var vocabulary = await _vocabularyRepository.GetByIdAsync(vocabularyId);
            if (vocabulary == null) return false;

            await _vocabularyRepository.RemoveFromVocabulariesListAsync (vocabulary, vocabulariesList);

            return true;
        }
    }
}
