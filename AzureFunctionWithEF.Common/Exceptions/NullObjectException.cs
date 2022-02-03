using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunctionWithEF.Common.Exceptions
{
    public class NullObjectException: Exception
    {
        /// <summary>
        /// Return an exeption if the object is null
        /// </summary>
        /// <param name="message"></param>
        public NullObjectException(string message): base(message) { }
    }
}
