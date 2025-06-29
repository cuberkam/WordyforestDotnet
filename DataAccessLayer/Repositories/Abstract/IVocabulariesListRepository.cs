using EntityLayer.Entities;

namespace WordyforestDotnet.DataAccessLayer.Repositories.Abstract
{
    public interface IVocabulariesListRepository
    {
        Task<VocabulariesList?> GetByIdWithVocabularies(int id);
        Task<VocabulariesList?> GetByShareCode(string shareCode);
        Task<VocabulariesList?>GetByIdAndByUserId(int id, string userId);
        Task<List<int>?> GetVocabulariesId(int id);
        Task<VocabulariesList?> Create(string vocabulariesListName, string userId);
        Task<bool> Delete(VocabulariesList? vocabularyList);
        Task<List<VocabulariesList>> GetPublicLists();
        Task<List<VocabulariesList>> GetUserLists(string userId);
    }
}