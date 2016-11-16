using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Exceptions
{
    public class EmptyGuidException : SystemException
    {
        private string _message;
        public override string Message
        {
            get
            {
                return _message;
            }
        }
        public new Exception InnerException { get; }
        public EmptyGuidException() : this(null, null)
        {

        }

        public EmptyGuidException(string message) : this(message, null)
        {
            _message = message;
        }

        public EmptyGuidException(string message, Exception innerException)
        {
            InnerException = innerException;
        }
    }
}
