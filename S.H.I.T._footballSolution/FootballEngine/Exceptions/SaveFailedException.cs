using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Helper
{
    class SaveFailedException : SystemException
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
        public SaveFailedException() : this(null, null)
        {

        }

        public SaveFailedException(string message) : this(message, null)
        {
            _message = message;
        }

        public SaveFailedException(string message, Exception innerException)
        {
            InnerException = innerException;
        }
    }
}
