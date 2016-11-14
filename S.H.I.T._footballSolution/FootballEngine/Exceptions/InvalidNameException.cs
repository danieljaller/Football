using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Exceptions
{
    [ComVisible(true)]
    public class InvalidNameException : SystemException
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
        public InvalidNameException() : this (null, null)
        {

        }

        public InvalidNameException(string message) : this (message, null)
        {
            _message = message;
        }

        public InvalidNameException(string message, Exception innerException)
        {
            InnerException = innerException;
        }
    }
}
