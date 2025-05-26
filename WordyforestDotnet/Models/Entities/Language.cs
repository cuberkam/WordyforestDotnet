namespace WordyforestDotnet.Models.Entities
{
    public class Language
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public required string EnglishName { get; set; }
        public required string NativeName { get; set; }
    }
}
