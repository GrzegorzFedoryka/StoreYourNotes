using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeYourNotes_webApi.Exceptions
{
    public class InsufficientStorageException : Exception
    {
        public InsufficientStorageException(string message) : base(message)
        {

        }
        public InsufficientStorageException() : base()
        {

        }
    }
}
