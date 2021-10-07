using API.Data;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context = context;
        }
        [HttpPost("register")]
        public async Task<ActionResult<Appuser>> Register(RegisterDTO registerDTO)
        {
            if(await UserExists(registerDTO.Username)) return BadRequest("Username is taken.");
            using var hmac = new  HMACSHA512();

            var user=new Appuser{
                UserName=registerDTO.Username.ToLower(),
                PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt=hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
        [HttpPost("login")]
        public async Task<ActionResult<Appuser>>Login(LoginDto loginDto)
        {
            var user = await _context.Users
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if(user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash= hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i=0; i<computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            return user;
        }
        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}