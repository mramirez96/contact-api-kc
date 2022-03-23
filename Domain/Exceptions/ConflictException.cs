namespace Domain.Exceptions
{
    public class ConflictException : BaseException
    {
        public ConflictException(string item, string reference)
            : base($"Conflict with {item} {reference}", System.Net.HttpStatusCode.Conflict)
        {

        }
    }
}
