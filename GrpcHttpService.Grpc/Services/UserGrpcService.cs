using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcHttpService.BusinessLogic.Interfaces.Service;
using GrpcHttpService.Grpc.Protos;

namespace GrpcHttpService.Grpc.Services
{
    /// <summary>
    /// Grpc service for users.
    /// </summary>
    public class UserGrpcService(IUserService userService) : UserService.UserServiceBase
    {
        private readonly IUserService _userService = userService;

        public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            var user = await _userService.CreateUserAsync(request.Name, request.Username, request.Email);
            return new CreateUserResponse { Id = user.Id.ToString() };
        }

        public override async Task<GetUserByIdResponse> GetUserById(GetUserByIdRequest request, ServerCallContext context)
        {
            var user = await _userService.GetUserByIdAsync(Guid.Parse(request.Id));
            if (user == null)
                throw new RpcException(new Status(StatusCode.NotFound, "User not found"));

            return new GetUserByIdResponse
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt.ToString("O"),
                UpdatedAt = user.UpdatedAt.ToString("O")
            };
        }

        public override async Task<Empty> DeleteUser(DeleteUserRequest request, ServerCallContext context)
        {
            await _userService.DeleteUserAsync(Guid.Parse(request.Id));
            return new Empty();
        }

        public override async Task<Empty> RestoreUser(RestoreUserRequest request, ServerCallContext context)
        {
            await _userService.RestoreUserAsync(Guid.Parse(request.Id));
            return new Empty();
        }
    }
}
