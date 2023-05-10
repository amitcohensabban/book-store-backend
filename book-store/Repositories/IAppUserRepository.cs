using book_store.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace book_store.Repositories
{
    public interface IAppUserRepository
    {
        Task<IdentityResult> SignUp(SignupModel signupModel);
        Task<string> Login(LoginModel loginModel);
        Task<string> DeleteUserAsync(string userId);
        Task<IdentityResult> UpdatePrivateDetails(UpdateDetailsModel newDetails);
        Task<bool> IsUserAdmin(string email);
    }
}
