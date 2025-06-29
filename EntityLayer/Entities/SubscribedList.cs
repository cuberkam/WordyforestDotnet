using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities
{
    public class SubscribedList
    {
        public int Id { get; set; }
        [ForeignKey("ExtendedUser")]
        public required string ExtendedUserId { get; set; }
        public int VocabulariesListId { get; set; }
        public ExtendedUser? ExtendedUser { get; set; }
        public VocabulariesList? VocabulariesList { get; set; }
    }
}
