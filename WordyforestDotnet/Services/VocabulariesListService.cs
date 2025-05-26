using Microsoft.EntityFrameworkCore;
using WordyforestDotnet.Data;
using WordyforestDotnet.Models.Entities;

namespace WordyforestDotnet.Services
{
    public class VocabulariesListService
    {
        private readonly ApplicationDbContext _context;
        public VocabulariesListService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VocabulariesList?> GetVocabulariesList(int vocabulariesId)
        {
            return await _context.VocabulariesLists.Include(vl => vl.Vocabularies).FirstOrDefaultAsync(vl => vl.Id == vocabulariesId);
        }

        public async Task<VocabulariesList?> GetVocabulariesListByShareCode(string shareCode)
        {
            return await _context.VocabulariesLists.FirstOrDefaultAsync(v => v.ShareId.ToString() == shareCode);
        }

        public async Task CreateVocabulariesList(VocabulariesList vocabulariesList, string userdId)
        {
            vocabulariesList.ExtendedUserId = userdId;
            _context.Add(vocabulariesList);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVocabulariesList(int vocabulariesListId, string userId)
        {
            VocabulariesList? vocabularyList = await _context.VocabulariesLists.FirstOrDefaultAsync(v => v.Id == vocabulariesListId && v.ExtendedUserId==userId);

            _context.VocabulariesLists.Remove(vocabularyList!);
            await _context.SaveChangesAsync();
        }

        public async Task<List<VocabulariesList>> GetPublicVocabulariesLists()
        {
            return await _context.VocabulariesLists.Where(vl => vl.IsPrivate == false).ToListAsync();
        }

        public async Task<List<VocabulariesList>> GetUserVocabulariesLists(string userId)
        {
            return await _context.VocabulariesLists.Where(vl => vl.ExtendedUserId == userId).ToListAsync();
        }

        public async Task<List<VocabulariesList>> GetUserSubscribedLists(string userId)
        {
            return await _context.SubscribedLists
                .Where(sl => sl.ExtendedUserId == userId)
                .Include(sl => sl.VocabulariesList)
                .Select(sl => sl.VocabulariesList!)
                .ToListAsync();
        }

        public async Task<bool> CreateSubscribedList(string userId, VocabulariesList vocabularyList)
        {
            SubscribedList? isSubscribedListExist =await _context.SubscribedLists.FirstOrDefaultAsync(sl => sl.VocabulariesListId == vocabularyList.Id && sl.ExtendedUserId == userId);
            if (isSubscribedListExist != null) return false;

            SubscribedList subscribedList = new()
            {
                ExtendedUserId = userId,
                VocabulariesList = vocabularyList
            };
            _context.Add(subscribedList);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteSubscribedList(string shareCode, string userId)
        {
            VocabulariesList? vocabularyList = await GetVocabulariesListByShareCode(shareCode);
            SubscribedList? subscribedList = await _context.SubscribedLists.FirstOrDefaultAsync(sl => sl.VocabulariesListId == vocabularyList!.Id & sl.ExtendedUserId == userId);

            _context.SubscribedLists.Remove(subscribedList!);
            await _context.SaveChangesAsync();
        }
    }
}
