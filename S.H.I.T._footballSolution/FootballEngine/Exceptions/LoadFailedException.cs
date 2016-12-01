using System;

namespace FootballEngine.Helper
{
    public class LoadFailedException : SystemException
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
        public LoadFailedException() : this(null, null)
        {

        }

        public LoadFailedException(string message) : this(message, null)
        {
            _message = message;
        }

        public LoadFailedException(string message, Exception innerException)
        {
            InnerException = innerException;
        }
    }
}
