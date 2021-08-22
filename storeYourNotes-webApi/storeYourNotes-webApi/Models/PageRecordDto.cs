using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeYourNotes_webApi.Models
{
    public class PageRecordDto
    {
        public PageRecordType Type { get; set; }
        public int Id { get; set; }
        public string Contents { get; set; }
        public PageRecordAction Action { get; set; }
    }
}
