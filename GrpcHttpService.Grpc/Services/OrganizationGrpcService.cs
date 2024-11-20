using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcHttpService.BusinessLogic.Interfaces.Service;
using GrpcHttpService.Grpc.Protos;

namespace GrpcHttpService.Grpc.Services
{
    /// <summary>
    /// Grpc service for organizations
    /// </summary>
    public class OrganizationGrpcService : OrganizationService.OrganizationServiceBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationGrpcService(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public override async Task<CreateOrganizationResponse> CreateOrganization(CreateOrganizationRequest request, ServerCallContext context)
        {
            var organization = await _organizationService.CreateOrganizationAsync(request.Name, request.Address);
            return new CreateOrganizationResponse { Id = organization.Id.ToString() };
        }

        public override async Task<GetOrganizationByIdResponse> GetOrganizationById(GetOrganizationByIdRequest request, ServerCallContext context)
        {
            var organization = await _organizationService.GetOrganizationByIdAsync(Guid.Parse(request.Id));
            if (organization == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Organization not found"));

            return new GetOrganizationByIdResponse
            {
                Id = organization.Id.ToString(),
                Name = organization.Name,
                Address = organization.Address,
                CreatedAt = organization.CreatedAt.ToString("O"),
                UpdatedAt = organization.UpdatedAt.ToString("O")
            };
        }

        public override async Task<Empty> DeleteOrganization(DeleteOrganizationRequest request, ServerCallContext context)
        {
            await _organizationService.DeleteOrganizationAsync(Guid.Parse(request.Id));
            return new Empty();
        }

        public override async Task<Empty> RestoreOrganization(RestoreOrganizationRequest request, ServerCallContext context)
        {
            await _organizationService.RestoreOrganizationAsync(Guid.Parse(request.Id));
            return new Empty();
        }
    }
}
