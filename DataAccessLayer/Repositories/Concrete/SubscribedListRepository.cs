using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using WordyforestDotnet.DataAccessLayer.Context;
using WordyforestDotnet.DataAccessLayer.Repositories.Abstract;

namespace WordyforestDotnet.DataAccessLayer.Repositories.Concrete
{
    public class SubscribedListRepository : ISubscribedListRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscribedListRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SubscribedList?> GetByVocabulariesListIdAndUserId(string userId, int vocabulariesListId)
        {
            return await _context.SubscribedLists.FirstOrDefaultAsync(sl => sl.VocabulariesListId == vocabulariesListId && sl.ExtendedUserId == userId);
        }

        public async Task<List<VocabulariesList>> GetUserSubscribedLists(string userId)
        {
            return await _context.SubscribedLists
                .Where(sl => sl.ExtendedUserId == userId)
                .Include(sl => sl.VocabulariesList)
                .Select(sl => sl.VocabulariesList!)
                .ToListAsync();
        }

        public async Task<VocabulariesList?> Create(string userId, VocabulariesList vocabulariesList)
        {
            SubscribedList subscribedList = new()
            {
                ExtendedUserId = userId,
                VocabulariesList = vocabulariesList
            };
            _context.Add(subscribedList);
            await _context.SaveChangesAsync();

            return vocabulariesList;
        }

        public async Task Delete(SubscribedList? subscribedList)
        {
            _context.SubscribedLists.Remove(subscribedList!);
            await _context.SaveChangesAsync();
        }
    }
}
