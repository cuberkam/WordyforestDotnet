using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace WordyforestDotnet.Models.Entities
{
    public class ExtendedUser : IdentityUser
    {
        public ICollection<VocabulariesList>? VocabulariesLists { get; set; } = new HashSet<VocabulariesList>();
        public ICollection<SubscribedList>? SubscribedLists { get; set; } = new HashSet<SubscribedList>();
    }
}
