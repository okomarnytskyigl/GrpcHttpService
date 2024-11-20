namespace GrpcHttpService.Domain.Exceptions
{
    /// <summary>
    /// Custom exception for domain errors.
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
