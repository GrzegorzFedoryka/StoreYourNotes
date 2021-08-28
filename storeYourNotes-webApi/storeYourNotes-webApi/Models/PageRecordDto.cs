using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeYourNotes_webApi.Models
{
    public class PageRecordDto
    {
        public int Id { get; set; }
        public int? PreviousRecordId { get; set; }
        public string Type { get; set; }
        public string Contents { get; set; }
    }
}
