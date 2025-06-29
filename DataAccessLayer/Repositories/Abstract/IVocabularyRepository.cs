using EntityLayer.Entities;

namespace WordyforestDotnet.DataAccessLayer.Repositories.Abstract
{
    public interface IVocabularyRepository
    {
        Task<Vocabulary?> GetByIdAsync(int id);
        Task<Vocabulary?> GetRandomAsync();
        Task<Vocabulary?> GetRandomByListIdAsync(int vocabulariesListId);
        Task<List<Vocabulary>> SearchWordAsync(string word, List<int>? excludedVocabularyIds);
        Task AddToVocabulariesListAsync(Vocabulary vocabulary, VocabulariesList vocabulariesList);
        Task RemoveFromVocabulariesListAsync(Vocabulary vocabulary, VocabulariesList vocabulariesList);
    }
}
