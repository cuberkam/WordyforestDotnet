using EntityLayer.Entities;

namespace WordyforestDotnet.BusinessLayer.Services.Abstract
{
    public interface IVocabularyService
    {
        Task<Vocabulary?> GetRandomVocabularyByVocabulariesList(int vocabulariesListId = -1);
        Task<List<Vocabulary>> SearchWordExcludeVocabulariesList(string word, int excludedListId);
        Task<Vocabulary?> AddVocabularyToVocabulariesList(int vocabulariesListId, int vocabularyId);
        Task<bool> RemoveVocabularyFromVocabulariesList(int vocabulariesListId, int vocabularyId);
    }
}
