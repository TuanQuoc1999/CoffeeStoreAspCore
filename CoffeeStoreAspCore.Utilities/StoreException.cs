using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.Utilities
{
   public class StoreException :Exception
    {
        public StoreException()
        {
        }

        public StoreException(string message)
            : base(message)
        {
        }

        public StoreException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
