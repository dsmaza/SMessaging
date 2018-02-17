using System;

namespace AspNetCoreCqrsSample.Domain
{
    public class BookingException : Exception
    {
        public BookingException(int status, string message) : base(message)
        {
            Status = status;
        }

        public int Status { get; }
    }

    public class BookingAlreadyExists : BookingException
    {
        public BookingAlreadyExists() : base(409, "Booking conflict")
        {

        }
    }

    public class BookingNotCreatedException : BookingException
    {
        public BookingNotCreatedException() : base(404, "Booking not created")
        {

        }
    }

    public class BookingNotConfirmedException : BookingException
    {
        public BookingNotConfirmedException() : base(400, "Booking should be confirmed first")
        {

        }
    }

    public class InvalidConfirmationCodeException : BookingException
    {
        public InvalidConfirmationCodeException() : base(400, "Provided code is invalid")
        {

        }
    }
}
