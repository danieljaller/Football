﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Exceptions
{
    public class InvalidNumberOfPlayersException : SystemException
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
        public InvalidNumberOfPlayersException() : this (null, null)
        {

        }

        public InvalidNumberOfPlayersException(string message) : this (message, null)
        {
            _message = message;
        }

        public InvalidNumberOfPlayersException(string message, Exception innerException)
        {
            InnerException = innerException;
        }
    }
}