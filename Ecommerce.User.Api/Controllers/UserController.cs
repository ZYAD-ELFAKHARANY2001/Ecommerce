using Ecommerce.Application.Contracts;
using Ecommerce.Application.Helpers;
using Ecommerce.Application.Services;
using Ecommerce.Context;
using Ecommerce.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ecommerce.User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EcommerceContext _context;
        private readonly UserManager<Ecommerce.Models.User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration configuration;
        private readonly JWT _jwt;
        private readonly IAuthServices _Auth;
        private readonly IUnitOfWork _UnitOfWork;

        public UserController(EcommerceContext context, UserManager<Ecommerce.Models.User> userManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, IOptions<JWT> jwt, IAuthServices Auth, IUnitOfWork UnitOfWork)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            this.configuration = configuration;
            _jwt = jwt.Value;
            _Auth = Auth;
            _UnitOfWork = UnitOfWork;

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Registeration([FromForm] RegisterDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _Auth.RegisterAsync(user);
            if (result.IsAuthenticated) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> loginAsync([FromForm] LoginDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _Auth.loginAsync(user);
            return Ok(result);
        }
        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            if (users == null) return BadRequest(ModelState);
            return Ok(users);
        }
    }
}
