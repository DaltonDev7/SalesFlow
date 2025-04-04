
using SalesFlow.Application.Dtos.Authentication;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Interfaces.Services
{
    public interface IAuthenticationServices
    {
        Task<ApiResponse<string>> RegisterUser(RegisterUser registerUser);
        Task<ApiResponse<List<GetUserAuth>>> GetUsers();

        Task<ApiResponse<AuthenticationResponse>> SignIn(SignInRequest request);
    }
}
