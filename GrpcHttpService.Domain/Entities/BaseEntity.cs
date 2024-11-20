namespace GrpcHttpService.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; } 
        public DateTime UpdatedAt { get; protected set; }
        public DateTime? DeletedAt { get; protected set; }
    }
}