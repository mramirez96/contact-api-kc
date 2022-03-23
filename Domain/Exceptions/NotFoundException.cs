namespace Domain.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string item, string reference)
            : base($"{item} with {reference} was not found.", System.Net.HttpStatusCode.NotFound)
        {
        }
    }
}
