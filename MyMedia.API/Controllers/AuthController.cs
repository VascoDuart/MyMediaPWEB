using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RCLComum.Models;
using MyMedia.API.Services;
using MyMedia.Data.Models;
using System.Threading.Tasks;

namespace MyMedia.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                NomeCompleto = model.NomeCompleto,
                IsAtivo = false,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, model.Role);

            if (!roleResult.Succeeded) {
                await _userManager.DeleteAsync(user);
                return BadRequest($"Falha ao atribuir o perfil.");
            }

            return StatusCode(201, new { Message = "Registo concluído com sucesso." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return Unauthorized(new { Message = "Credenciais inválidas." });

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
                return Unauthorized(new { Message = "Credenciais inválidas." });

            if (!user.IsAtivo)
                return Unauthorized(new { Message = "A sua conta ainda não foi aprovada pelo administrador." });

            var token = await _tokenService.CreateToken(user);

            return Ok(new LoginResult{
                Token = token,
                Nome = user.NomeCompleto,
                Email = user.Email,
                Sucedido = true
            });
        }
    }
}