using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Entities
{
    public class ExtendedUser : IdentityUser
    {
        public ICollection<VocabulariesList>? VocabulariesLists { get; set; } = new HashSet<VocabulariesList>();
        public ICollection<SubscribedList>? SubscribedLists { get; set; } = new HashSet<SubscribedList>();
    }
}
