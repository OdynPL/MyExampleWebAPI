using Microsoft.EntityFrameworkCore;
using MyExampleWebAPI.Models;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<IEnumerable<Member>> GetAllMembersAsync()
    {
        return await _memberRepository.GetAllMembersAsync();
    }


    public async Task<Member> GetMemberByIdAsync(Guid id)
    {
        return await _memberRepository.GetMemberByIdAsync(id);
    }

    public async Task AddMemberAsync(Member member)
    {
        await _memberRepository.AddMemberAsync(member);
    }

    public async Task UpdateMemberAsync(Member member)
    {
        await _memberRepository.UpdateMemberAsync(member);
    }

    public async Task DeleteMemberAsync(Guid id)
    {
        await _memberRepository.DeleteMemberAsync(id);
    }

    public async Task<IEnumerable<Member>> GetMembersPaginated(int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;

        // Teraz paginacja jest realizowana w zapytaniu SQL
        return await _memberRepository.GetMembersPaginatedAsync(skip, pageSize);
    }

    public async Task<int> GetTotalMembersCount()
    {
        return await _memberRepository.GetTotalMembersCountAsync();
    }
}
