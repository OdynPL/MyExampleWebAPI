using MyExampleWebAPI.Models;

/// <summary>
/// Member Repository with correspondign methods (basic cruds)
/// </summary>
public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllMembersAsync();
    Task<IEnumerable<Member>> GetMembersPaginatedAsync(int skip, int take);
    Task<Member> GetMemberByIdAsync(Guid id);
    Task AddMemberAsync(Member member);
    Task UpdateMemberAsync(Member member);
    Task DeleteMemberAsync(Guid id);
    Task<int> GetTotalMembersCountAsync();
}
