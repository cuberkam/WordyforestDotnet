using EntityLayer.Entities;

namespace WordyforestDotnet.BusinessLayer.Services.Abstract
{
    public interface IVocabulariesListService
    {
        Task<VocabulariesList?> GetVocabulariesList(int vocabulariesId);
        Task<VocabulariesList?> GetVocabulariesListByShareCode(string shareCode);
        Task<VocabulariesList?> CreateVocabulariesList(string vocabulariesListName, string userdId);
        Task<bool> DeleteVocabulariesList(int vocabulariesListId, string userId);
        Task<List<VocabulariesList>> GetPublicVocabulariesLists();
        Task<List<VocabulariesList>> GetUserVocabulariesLists(string userId);
    }
}
