using book_store.Models;
using Microsoft.AspNetCore.Identity;

namespace book_store.Repositories
{
    public interface IAppUserRepository
    {
        Task<IdentityResult> SignUp(SignupModel signupModel);
        Task<string> Login(LoginModel loginModel);

    }
}
