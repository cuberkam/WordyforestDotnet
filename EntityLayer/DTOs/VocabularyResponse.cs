using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordyforestDotnet.EntityLayer.DTOs
{
    public class VocabularyResponse
    {
        public int Id { get; set; }
        public required string Word { get; set; }
        public required string Description { get; set; }
    }
}
