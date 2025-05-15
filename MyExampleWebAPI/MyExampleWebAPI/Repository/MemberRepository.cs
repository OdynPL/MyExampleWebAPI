using Microsoft.EntityFrameworkCore;
using MyExampleWebAPI.Models;

public class MemberRepository : IMemberRepository
{
    private readonly AppDbContext _context;

    public MemberRepository(AppDbContext context)
    {
        _context = context;
    }

    // Get all members from database DTO
    public async Task<IEnumerable<Member>> GetAllMembersAsync()
    {
        return await _context.MembersDTO.ToListAsync();
    }

    // Get only paginated members (from skip to take)
    public async Task<IEnumerable<Member>> GetMembersPaginatedAsync(int skip, int take)
    {
        return await _context.MembersDTO
                             .Skip(skip) 
                             .Take(take)
                             .ToListAsync();
    }

    // Get member object by his GUID
    public async Task<Member> GetMemberByIdAsync(Guid id)
    {
        var member = await _context.MembersDTO.FirstOrDefaultAsync(m => m.Id == id);

        if (member == null) 
            return null;

        return member;
    }

    // Creat new member entry 
    public async Task AddMemberAsync(Member member)
    {
        await _context.MembersDTO.AddAsync(member);
        await _context.SaveChangesAsync();
    }

    // Update member object (Todo in future)
    public async Task UpdateMemberAsync(Member member)
    {
        _context.MembersDTO.Update(member);
        await _context.SaveChangesAsync();
    }

    // Remove member from database
    public async Task DeleteMemberAsync(Guid id)
    {
        var member = await _context.MembersDTO.FindAsync(id);
        if (member != null)
        {
            _context.MembersDTO.Remove(member);
            await _context.SaveChangesAsync();
        }
    }

    // Count members
    public async Task<int> GetTotalMembersCountAsync()
    {
        return await _context.MembersDTO.CountAsync();
    }
}
