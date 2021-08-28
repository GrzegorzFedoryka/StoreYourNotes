using storeYourNotes_webApi.Entities;
using storeYourNotes_webApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace storeYourNotes_webApi.Entities
{
    public class PageRecord
    {
        public int Id { get; set; }
        public int? PreviousRecordId { get; set; }
        public int? NextRecordId { get; set; }
        public string Type { get; set; }
        public string Contents { get; set; }
        public int PageId { get; set; }
        [JsonIgnore]
        public virtual Page Page { get; set; }
    }
}
