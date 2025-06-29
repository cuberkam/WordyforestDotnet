using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WordyforestDotnet.DataAccessLayer.Context;
using WordyforestDotnet.DataAccessLayer.Repositories.Abstract;

namespace WordyforestDotnet.DataAccessLayer.Repositories.Concrete
{
    public class VocabulariesListRepository : IVocabulariesListRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ExtendedUser> _userManager;

        public VocabulariesListRepository(ApplicationDbContext context, UserManager<ExtendedUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<VocabulariesList?> GetByIdWithVocabularies(int id)
        {
            return await _context.VocabulariesLists.Include(vl => vl.Vocabularies).FirstOrDefaultAsync(vl => vl.Id == id);
        }

        public async Task<VocabulariesList?> GetByShareCode(string shareCode)
        {
            return await _context.VocabulariesLists.FirstOrDefaultAsync(v => v.ShareId.ToString() == shareCode);
        }

        public async Task<VocabulariesList?> GetByIdAndByUserId(int id, string userId)
        {
            return await _context.VocabulariesLists.FirstOrDefaultAsync(v => v.Id == id && v.ExtendedUserId == userId);
        }

        public async Task<List<int>?> GetVocabulariesId(int id)
        {
            return await _context.VocabulariesLists
                .Where(vl => vl.Id == id)
                .SelectMany(vl => vl.Vocabularies!.Select(v => v.Id))
                .ToListAsync();
        }

        public async Task<VocabulariesList?> Create(string vocabulariesListName, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var vocabulariesList = new VocabulariesList()
            {
                Name = vocabulariesListName,
                ExtendedUserId = userId,
                ExtendedUser = user!
            };

            _context.Add(vocabulariesList);
            await _context.SaveChangesAsync();

            return vocabulariesList!;
        }

        public async Task<bool> Delete(VocabulariesList? vocabularyList)
        {
            try
            {
                _context.VocabulariesLists.Remove(vocabularyList!);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public async Task<List<VocabulariesList>> GetPublicLists()
        {
            return await _context.VocabulariesLists.Where(vl => vl.IsPrivate == false).ToListAsync();
        }

        public async Task<List<VocabulariesList>> GetUserLists(string userId)
        {
            return await _context.VocabulariesLists
                        .AsNoTracking()
                        .Where(vl => vl.ExtendedUser.Id == userId)
                        .ToListAsync(); ;
        }
    }
}
