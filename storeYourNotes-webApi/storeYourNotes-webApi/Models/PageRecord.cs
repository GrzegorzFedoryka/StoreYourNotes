using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeYourNotes_webApi.Models
{
    public class PageRecord
    {
        public PageRecordType Type { get; set; }
        public string JsonText { get; set; }
    }
}
