using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace storeYourNotes_webApi.Entities
{
    public class Page
    {
        public int Id { get; set; }
        public int? PageUpId { get; set; }
        public int OwnerId { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual List<PageRecord> PageRecords { get; set; }
    }
}
