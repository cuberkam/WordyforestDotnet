namespace EntityLayer.Entities
{
    public class Synonym
    {
        public int Id { get; set; }
        public required string Type { get; set; }
        public required List<string> Words { get; set; }
        public Vocabulary? Vocabulary { get; set; }
    }
}
