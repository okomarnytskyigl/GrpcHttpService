using GrpcHttpService.Grpc.Protos;

namespace GrpcHttpService.Http.Endpoints
{
    public static class UserEndpoints
    {
        /// <summary>
        /// User grpc endpoints
        /// </summary>
        /// <param name="app">WebApplication</param>
        public static void MapUserGrpcEndpoints(this WebApplication app)
        {
            app.MapPost("/api/user", async (CreateUserRequest request, UserService.UserServiceClient grpcClient) =>
            {
                var response = await grpcClient.CreateUserAsync(request);
                return Results.Ok(response);
            });

            app.MapGet("/api/user/{id}", async (string id, UserService.UserServiceClient grpcClient) =>
            {
                var response = await grpcClient.GetUserByIdAsync(new GetUserByIdRequest { Id = id });
                return Results.Ok(response);
            });

            app.MapDelete("/api/user/{id}", async (string id, UserService.UserServiceClient grpcClient) =>
            {
                await grpcClient.DeleteUserAsync(new DeleteUserRequest { Id = id });
                return Results.NoContent();
            });

            app.MapPost("/api/user/{id}/restore", async (string id, UserService.UserServiceClient grpcClient) =>
            {
                await grpcClient.RestoreUserAsync(new RestoreUserRequest { Id = id });
                return Results.NoContent();
            });
        }
    }
}
