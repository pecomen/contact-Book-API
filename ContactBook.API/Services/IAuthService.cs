using ContactBook.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ContactBook.Services
{
    public interface IAuthService
    {
        string GenerateJWT(AppUser user);
        Task<SignInResult> Login(AppUser user, string password);
    }
}
