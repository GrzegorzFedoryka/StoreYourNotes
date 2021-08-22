using storeYourNotes_webApi.Entities;
using storeYourNotes_webApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeYourNotes_webApi.Entities
{
    public class PageRecord
    {
        public int Id { get; set; }
        public PageRecordType Type { get; set; }
        public string Contents { get; set; }
        public int PageId { get; set; }
        public virtual Page Page { get; set; }
    }
}
