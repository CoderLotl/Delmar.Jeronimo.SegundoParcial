using System;
using System.Runtime.Serialization;

namespace Library
{
    [Serializable]
    public class EmptyDeckException : Exception
    {
        public EmptyDeckException()
        {
        }

        public EmptyDeckException(string message) : base(message)
        {

        }
    }
}