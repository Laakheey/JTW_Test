using Jwt.Model;
using Microsoft.Win32;

namespace Jwt.Repository
{
    public interface IJWTManagerRepository
    {

        Task<LoginResponseModel> Login(Login model);

        Task<LoginResponseModel> SignUp(Register model);

    }
}
