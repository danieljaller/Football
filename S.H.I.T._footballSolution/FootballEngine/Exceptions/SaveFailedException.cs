using System;

namespace FootballEngine.Helper
{
    public class SaveFailedException : SystemException
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