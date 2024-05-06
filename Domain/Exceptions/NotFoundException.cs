namespace Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, int id)
            : base($"{entityName} not found for id {id}")
        {

        }
    }
}