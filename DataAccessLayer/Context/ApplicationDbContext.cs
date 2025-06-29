using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EntityLayer.Entities;

namespace WordyforestDotnet.DataAccessLayer.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Synonym> Synonyms { get; set; }
        public DbSet<Vocabulary> Vocabularies { get; set; }
        public DbSet<VocabulariesList> VocabulariesLists { get; set; }
        public DbSet<SubscribedList> SubscribedLists { get; set; }
        public DbSet<ExtendedUser> ExtendedUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VocabulariesList>(entity =>
            {
                entity.HasIndex(v => new { v.Name, v.ExtendedUserId }).IsUnique();
            });

            modelBuilder.Entity<SubscribedList>(entity =>
            {
                entity.HasIndex(s => new { s.ExtendedUserId, s.VocabulariesListId }).IsUnique();
            });
        }
    }
}
