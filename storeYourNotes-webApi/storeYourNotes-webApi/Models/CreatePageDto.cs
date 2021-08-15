using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeYourNotes_webApi.Models
{
    public class CreatePageDto
    {
        public int PageUpId { get; set; }
        public int OwnerId { get; set; }
    }
}
