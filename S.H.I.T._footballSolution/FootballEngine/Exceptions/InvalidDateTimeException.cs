using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Exceptions
{
    public class InvalidDateTimeException : SystemException
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
        public InvalidDateTimeException() : this(null, null)
        {

        }

        public InvalidDateTimeException(string message) : this(message, null)
        {
            _message = message;
        }

        public InvalidDateTimeException(string message, Exception innerException)
        {
            InnerException = innerException;
        }
    }
}
