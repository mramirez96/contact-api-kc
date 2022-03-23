namespace Domain.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(string message)
            : base(message, System.Net.HttpStatusCode.BadRequest)
        {

        }
    }
}
