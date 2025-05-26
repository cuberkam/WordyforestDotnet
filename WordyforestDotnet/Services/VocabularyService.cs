using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WordyforestDotnet.Data;
using WordyforestDotnet.Models.Entities;
using Azure.Core;

namespace WordyforestDotnet.Services
{
    public class VocabularyService
    {
        private readonly ApplicationDbContext _context;
        public VocabularyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Vocabulary> GetRandomVocabularyByVocabulariesList(int vocabulariesListId= -1)
        {
            if (vocabulariesListId == -1)
            {
                var count = _context.Vocabularies.Count();
                var index = new Random().Next(0, count);
                return (await _context.Vocabularies.Include(v => v.Synonyms).Skip(index).FirstOrDefaultAsync())!;
            }

            var vocabulary = await _context.Vocabularies
                .Where(v => v.VocabulariesLists!.Any(vl => vl.Id == vocabulariesListId))
                .Include(v => v.Synonyms)
                .OrderBy(x => EF.Functions.Random())
                .FirstOrDefaultAsync();

            return vocabulary!;
        }

        public async Task<List<Vocabulary>> SearchVocabulariesByVocabulariesList(string word, int excludedListId)
        {
            var excludedVocabularyIds = await _context.VocabulariesLists
                .Where(vl => vl.Id == excludedListId)
                .SelectMany(vl => vl.Vocabularies!.Select(v => v.Id))
                .ToListAsync();

            var result = await _context.Vocabularies
                .Where(i => i.Word.Contains(word))
                .Where(i => !excludedVocabularyIds.Contains(i.Id))
                .OrderBy(i => i.Word.IndexOf(word))
                .Take(30)
                .ToListAsync();

            return result;
        }

        public async Task<Vocabulary?> AddVocabularyToVocabulariesList(int vocabulariesListId, int vocabularyId)
        {
            var vocabulariesList = await _context.VocabulariesLists
            .Include(vl => vl.Vocabularies)
            .FirstOrDefaultAsync(vl => vl.Id == vocabulariesListId);
            if (vocabulariesList == null) return null;

            var vocabulary = await _context.Vocabularies.FindAsync(vocabularyId);
            if (vocabulary == null) return null;

            vocabulariesList.Vocabularies!.Add(vocabulary);
            await _context.SaveChangesAsync();

            return vocabulary;


        }

        public async Task<bool> RemoveVocabularyFromVocabulariesList(int vocabulariesListId, int vocabularyId)
        {
            var vocabulariesList = await _context.VocabulariesLists
                .Include(vl => vl.Vocabularies)
                .FirstOrDefaultAsync(vl => vl.Id == vocabulariesListId);
            if (vocabulariesList == null) return false;

            var vocabulary = await _context.Vocabularies.FindAsync(vocabularyId);
            if (vocabulary == null) return false;

            vocabulariesList.Vocabularies!.Remove(vocabulary);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
