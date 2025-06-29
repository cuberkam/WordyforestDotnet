using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities
{
    public class VocabulariesList
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsPrivate { get; set; } = true;
        public Guid ShareId { get; set; } = Guid.NewGuid();
        [ForeignKey("ExtendedUser")]
        public required string  ExtendedUserId { get; set; }
        public required ExtendedUser ExtendedUser { get; set; }
        public ICollection<Vocabulary>? Vocabularies { get; set; } = new HashSet<Vocabulary>();
    }
}
