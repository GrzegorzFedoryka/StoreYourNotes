using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeYourNotes_webApi.Entities
{
    public class Page
    {
        public int Id { get; set; }
        public string JsonRecords { get; set; }
        public string OwnerId { get; set; }
        public int PageUpId { get; set; }
    }
}
