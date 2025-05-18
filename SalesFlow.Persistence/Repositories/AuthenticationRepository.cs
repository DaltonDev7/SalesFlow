

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Dtos.Authentication;
using SalesFlow.Application.Exception;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Interfaces.Services;
using SalesFlow.Application.Models;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Entities;
using SalesFlow.Persistence.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SalesFlow.Persistence.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly ApplicationContext _identityContext;
        private readonly IRolesServices _rolesServices;

        public AuthenticationRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JWTSettings> jwtSettings,
            ApplicationContext identity,
            IRolesServices rolesServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityContext = identity;
            _jwtSettings = jwtSettings.Value;
            _rolesServices = rolesServices;
        }

        public async Task<ApiResponse<AuthenticationResponse>> GetUserByIdAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return new ApiResponse<AuthenticationResponse>()
                {
                    Message = "Usuario no encontrado.",
                    Succeeded = false
                };
            }

            var rolesUser = await _rolesServices.GetUserRolesAsync(user.Id);

            AuthenticationResponse response = new()
            {
                Id = user.Id,
                Email = user.Email,
                Names = user.Names,
                LastNames = user.LastNames,
                Roles = rolesUser
            };

            return new ApiResponse<AuthenticationResponse>(response);
        }


        public async Task<ApiResponse<List<GetUserAuth>>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userAuthList = new List<GetUserAuth>();

            foreach (var user in users)
            {
                var roles = await _rolesServices.GetUserRolesAsync(user.Id); // Obtiene los roles del usuario

                var userAuth = new GetUserAuth
                {
                    Id = user.Id,
                    Email = user.Email,
                    Names = user.Names,
                    LastNames = user.LastNames,
                    Gender = user.Gender,
                    PhoneNumber = user.PhoneNumber,
                    Status = user.Status,
                    Roles = roles
                };

                userAuthList.Add(userAuth);
            }

            return new ApiResponse<List<GetUserAuth>>(userAuthList);
        }

        public async Task<ApiResponse<string>> RegisterUser(RegisterUser registerUser)
        {
            var existingUser = await _userManager.FindByNameAsync(registerUser.Email);
            if (existingUser != null)
            {
                return new ApiResponse<string>()
                {
                    Message = "El usuario ya existe.",
                    Succeeded = false
                };
            }

            // Crear el usuario
            var user = new ApplicationUser
            {
                Email = registerUser.Email,
                EmailConfirmed = true,
                UserName = registerUser.Names + registerUser.LastNames,
                PhoneNumber = registerUser.PhoneNumber,
                Names = registerUser.Names,
                LastNames = registerUser.LastNames,
                Gender = registerUser.Gender,
                Status = registerUser.Status,
                Created = DateTime.UtcNow
            };

            // Crear usuario con contraseña
            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ApiResponse<string>()
                {
                    Message = $"Error al crear usuario: {errors}",
                    Succeeded = false
                };
            }

            // Asignar rol al usuario
            var role = await _identityContext.Roles.FindAsync(registerUser.IdRol);
            if (role == null)
            {
                return new ApiResponse<string>()
                {
                    Message = $"El rol especificado no existe.",
                    Succeeded = false
                };
            }

            var responseRole = await _userManager.AddToRoleAsync(user, role.Name);

            if (responseRole.Succeeded)
            {
                return new ApiResponse<string>("Usuario registrado exitosamente.");
            }
            else
            {
                return new ApiResponse<string>("Ocurrio un error");
            }

        }

        public async Task<ApiResponse<AuthenticationResponse>> SignIn(SignInRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                throw new ApiException("Credenciales incorrectas.", (int)HttpStatusCode.Unauthorized);
            }

            // Verificar si la contraseña es correcta
            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
            {
                throw new ApiException("Credenciales incorrectas.", (int)HttpStatusCode.Unauthorized);
            }

            // Verificar si el usuario está activo
            if (!user.Status)
            {
                throw new ApiException("Usuario inactivo. Contacte al administrador.", (int)HttpStatusCode.Forbidden);
            }

            // Generar JWT
            JwtSecurityToken jwtSecurityToken = await GetSecurityToken(user);
            response.JwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);


            var rolesUser = await _rolesServices.GetUserRolesAsync(user.Id);

            response.Id = user.Id;
            response.Email = user.Email;
            response.Names = user.Names;
            response.LastNames = user.LastNames;
            response.Roles = rolesUser;
            // Generar Refresh Token
            //var refreshToken = GenerateRefreshToken();
            //response.RefreshToken = refreshToken.Token;

            return new ApiResponse<AuthenticationResponse>(response);
        }

        private async Task<JwtSecurityToken> GetSecurityToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }


            var claims = new[]
            {
                  new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmectricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredetials = new SigningCredentials(symmectricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredetials);

            return jwtSecurityToken;
        }
    }
}
