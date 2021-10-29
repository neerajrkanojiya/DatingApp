using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interface
{
    public interface IUserRepository
    {
        void Update(Appuser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<Appuser>> GetUsersAsync();
        Task<Appuser> GetUserByIdAsync(int Id);
        Task<Appuser> GetUserByUsernameAsync(string username);
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<MemberDto> GetMemberAsync(string username);

    }
}