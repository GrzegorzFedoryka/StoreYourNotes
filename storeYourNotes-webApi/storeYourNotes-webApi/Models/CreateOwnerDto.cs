using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeYourNotes_webApi.Models
{
    public class CreateOwnerDto
    {
        public string Nickname { get; set; }
        public string Email { get; set; }
    }
}
