using System;

namespace Common.Exceptions
{
    public class CustomException : Exception
    {
        private string _message;

        public string AdditionalInformation { get; }

        public CustomException(string message)
        {
            _message = message;
        }

        public CustomException(string message, string additionalInformation)
        {
            _message = message;
            AdditionalInformation = additionalInformation;
        }

        public override string Message => _message;
    }
}
