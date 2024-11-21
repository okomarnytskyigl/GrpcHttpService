using GrpcHttpService.Grpc.Protos;

namespace GrpcHttpService.Http.Endpoints
{
    public static class OrganizationEndpoints
    {
        /// <summary>
        /// Organization grpc endpoints
        /// </summary>
        /// <param name="app">WebApplication</param>
        public static void MapOrganizationGrpcEndpoints(this WebApplication app)
        {
            // Организации
            app.MapPost("/api/organization", async (CreateOrganizationRequest request, OrganizationService.OrganizationServiceClient grpcClient) =>
            {
                var response = await grpcClient.CreateOrganizationAsync(request);
                return TypedResults.Ok(response);
            });

            app.MapGet("/api/organization/{id}", async (string id, OrganizationService.OrganizationServiceClient grpcClient) =>
            {
                var response = await grpcClient.GetOrganizationByIdAsync(new GetOrganizationByIdRequest { Id = id });
                return TypedResults.Ok(response);
            });

            app.MapDelete("/api/organization/{id}", async (string id, OrganizationService.OrganizationServiceClient grpcClient) =>
            {
                await grpcClient.DeleteOrganizationAsync(new DeleteOrganizationRequest { Id = id });
                return TypedResults.NoContent();
            });

            app.MapPost("/api/organization/{id}/restore", async (string id, OrganizationService.OrganizationServiceClient grpcClient) =>
            {
                await grpcClient.RestoreOrganizationAsync(new RestoreOrganizationRequest { Id = id });
                return TypedResults.NoContent();
            });
        }
    }
}
