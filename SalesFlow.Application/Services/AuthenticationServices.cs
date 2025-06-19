using SalesFlow.Application.Dtos.Authentication;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Interfaces.Services;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IAuthenticationRepository _authenticationServices;

        public AuthenticationServices(IAuthenticationRepository authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        public async Task<ApiResponse<AuthenticationResponse>> GetUserByIdAsync(int userId)
        {
           return await _authenticationServices.GetUserByIdAsync(userId);
        }

        public async Task<ApiResponse<List<GetUserAuth>>> GetUsers()
        {
            return await _authenticationServices.GetUsers();
        }  
        
        public async Task<ApiResponse<string>> UpdateUser(UpdateUserDto updateUser)
        {
            return await _authenticationServices.UpdateUser(updateUser);
        }

        public async Task<ApiResponse<string>> RegisterUser(RegisterUser registerUser)
        {
            return await _authenticationServices.RegisterUser(registerUser);
        }

        public async Task<ApiResponse<AuthenticationResponse>> SignIn(SignInRequest request)
        {
            return await _authenticationServices.SignIn(request);
        }
    }
}
