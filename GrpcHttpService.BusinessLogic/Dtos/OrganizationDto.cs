namespace GrpcHttpService.BusinessLogic.Dtos
{
    /// <summary>
    /// Organization Dto
    /// </summary>
    public class OrganizationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
