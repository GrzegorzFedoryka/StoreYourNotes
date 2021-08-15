using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeYourNotes_webApi.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
    }
}
