using Core.Concretes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Responses;

namespace Core.Abstract.IServices
{
    public interface IAuthService
    {
        Task<IResult> LoginAsync(LoginDTO model);
        Task<IResult> RegisterAsync(RegisterDTO model, bool isAdmin = false);
        Task<IResult> LogoutAsync();
        Task<IResult> ChangePasswordAsync(string oldPassword, string newPassword, string confirmPassword);
        Task<IResult> ResetPasswordAsync(string newPassword, string confirmPassword, string auth_token);
        Task<IResult> ForgotPasswordAsync(string email);
    }
}
