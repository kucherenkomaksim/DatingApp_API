using API.DTO;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);

    Task<bool> SaveAllAsync();

    Task<IEnumerable<AppUser>> GetUsersAsync(CancellationToken cancellationToken);

    Task<AppUser?> GetUserByIdAsync(long id);

    Task<AppUser?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken);

    Task<IEnumerable<MemberDto>> GetMembersAsync();

    Task<MemberDto?> GetMemberAsync(string userName);
}