using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using WordyforestDotnet.DataAccessLayer.Context;
using WordyforestDotnet.DataAccessLayer.Repositories.Abstract;

namespace WordyforestDotnet.DataAccessLayer.Repositories.Concrete
{
    public class VocabularyRepository : IVocabularyRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IVocabulariesListRepository _vocabulariesListRepository;

        public VocabularyRepository(ApplicationDbContext context, IVocabulariesListRepository vocabulariesListRepository)
        {
            _context = context;
            _vocabulariesListRepository = vocabulariesListRepository;
        }

        public async Task<Vocabulary?> GetByIdAsync(int id)
        {
            return await _context.Vocabularies.FindAsync(id);
        }

        public async Task<Vocabulary?> GetRandomAsync()
        {
            var count = await _context.Vocabularies.CountAsync();
            var index = new Random().Next(0, count);

            return await _context.Vocabularies
                .Include(v => v.Synonyms)
                .Skip(index)
                .FirstOrDefaultAsync();
        }

        public async Task<Vocabulary?> GetRandomByListIdAsync(int vocabulariesListId)
        {
            return await _context.Vocabularies
                .Where(v => v.VocabulariesLists!.Any(vl => vl.Id == vocabulariesListId))
                .Include(v => v.Synonyms)
                .OrderBy(x => EF.Functions.Random())
                .FirstOrDefaultAsync();
        }

        public async Task<List<Vocabulary>> SearchWordAsync(string word, List<int>? excludedVocabularyIds)
        {
            return await _context.Vocabularies
                .Where(i => i.Word.Contains(word))
                .Where(i => !excludedVocabularyIds!.Contains(i.Id))
                .OrderBy(i => i.Word.IndexOf(word))
                .Take(30)
                .ToListAsync();
        }
        public async Task AddToVocabulariesListAsync(Vocabulary vocabulary, VocabulariesList vocabulariesList)
        {
            vocabulariesList.Vocabularies!.Add(vocabulary);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromVocabulariesListAsync(Vocabulary vocabulary, VocabulariesList vocabulariesList)
        {
            vocabulariesList.Vocabularies!.Remove(vocabulary);
            await _context.SaveChangesAsync();
        }
    }
}