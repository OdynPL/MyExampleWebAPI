using MyExampleWebAPI.Models;

/// <summary>
/// Member service layer used to call our lower repository layer
/// with basic methoods declaration used inside controler
/// </summary>
public interface IMemberService
{
    Task<IEnumerable<Member>> GetAllMembersAsync();

    Task<IEnumerable<Member>> GetMembersPaginated(int page, int pageSize);
    Task<Member> GetMemberByIdAsync(Guid id);
    Task AddMemberAsync(Member member);
    Task UpdateMemberAsync(Member member);
    Task DeleteMemberAsync(Guid id);

    Task<int> GetTotalMembersCount();
}
