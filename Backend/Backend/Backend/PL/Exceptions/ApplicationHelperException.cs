using Backend.BL.Enums;
using System.Runtime.Serialization;

namespace Backend.PL.Exceptions
{
    [Serializable]
    internal class ApplicationHelperException : Exception
    {
        public ServiceResultType ErrorStatus { get; set; }
        public ApplicationHelperException()
        {
        }

        public ApplicationHelperException(ServiceResultType errorStatus, string? message) : base(message)
        {
            ErrorStatus = errorStatus;
        }

        public ApplicationHelperException(ServiceResultType errorStatus, string? message, Exception? innerException) : base(message, innerException)
        {
            ErrorStatus = errorStatus;
        }

        protected ApplicationHelperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}