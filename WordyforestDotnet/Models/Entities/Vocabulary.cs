using Microsoft.Extensions.Hosting;

namespace WordyforestDotnet.Models.Entities
{
    public class Vocabulary
    {
        public int Id { get; set; }
        public required string Word { get; set; }
        public required string Type { get; set; }
        public string? Description { get; set; }
        public string? Example { get; set; }
        public ICollection<Synonym>? Synonyms { get; set; } = new HashSet<Synonym>();
        public ICollection<VocabulariesList>? VocabulariesLists { get; set; } = new HashSet<VocabulariesList>();
    }
}
