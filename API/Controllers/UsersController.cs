using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Interface;
using API.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace API.Controllers
{
     [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();
            return Ok(users);
        }
        [HttpGet("{Username}")]
        public async Task<ActionResult<MemberDto>> GetUsers(string username)
        {
            return await _userRepository.GetMemberAsync(username);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            var user = await _userRepository.GetUserByUsernameAsync(username);

            _mapper.Map(memberUpdateDto, user);

            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }
        // public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        // {          
        //     var username = User.FindFirst(ClaimTypes.Country)?.Value;
        //     var username1 = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //     var username2 = User.FindFirst(ClaimTypes.GivenName)?.Value;
        //     var username3 = User.FindFirst(ClaimTypes.Name)?.Value;
        //     var username4 = User.FindFirst(ClaimTypes.UserData)?.Value;
            
        //     if (username == null) return BadRequest("User not found in Claim.");
        //     var user = await _userRepository.GetUserByUsernameAsync(username);
        //     if (user == null) return BadRequest("User not found.");
        //     _mapper.Map(memberUpdateDto, user);
        //     _userRepository.Update(user);

        //     if (await _userRepository.SaveAllAsync()) return NoContent();

        //     return BadRequest("Failed to update user.");
        // }
    }
}