

using SalesFlow.Application.Dtos.Authentication;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Interfaces.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<ApiResponse<string>> RegisterUser(RegisterUser registerUser);
        Task<ApiResponse<List<GetUserAuth>>> GetUsers();

        Task<ApiResponse<AuthenticationResponse>> SignIn(SignInRequest request);
    }
}
