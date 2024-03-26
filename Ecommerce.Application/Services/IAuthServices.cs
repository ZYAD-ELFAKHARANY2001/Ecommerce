using Ecommerce.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public interface IAuthServices
    {
        Task<AuthModel> RegisterAsync(RegisterDTO user);
        Task<AuthModel> loginAsync(LoginDTO user);
    }
}
