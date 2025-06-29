using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordyforestDotnet.EntityLayer.DTOs
{
    public class SubscribedListResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
