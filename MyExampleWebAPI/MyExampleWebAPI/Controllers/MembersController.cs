using Microsoft.AspNetCore.Mvc;
using MyExampleWebAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;
    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpGet]
    // Endpoint to pull all members from database GET https://localhost:7157/api/Members/
    public async Task<IActionResult> GetAllMembers()
    {
        var members = await _memberService.GetAllMembersAsync();
        return Ok(members);
    }

    [HttpGet("paginated")]
    // Get all paginated members GET https://localhost:7157/api/Members/paginated
    public async Task<IActionResult> GetMembersPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 8)
    {
        page = page < 1 ? 1 : page;
        pageSize = pageSize < 1 || pageSize > 100 ? 8 : pageSize;

        var totalMembersCount = await _memberService.GetTotalMembersCount(); 
        var totalPages = (int)Math.Ceiling((double)totalMembersCount / pageSize);

        var membersResult = await _memberService.GetMembersPaginated(page, pageSize);

        if (!membersResult.Any())
        {
            return NotFound("No members records found.");
        }

        var paginationResponse = new
        {
            Members = membersResult,
            TotalMembersCount = totalMembersCount,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize
        };

        return Ok(paginationResponse);
    }

    [HttpGet("{id}")]
    // Get member by GUID ID (SINGLE OBJECT)
    public async Task<IActionResult> GetMemberById(Guid id)
    {
        var member = await _memberService.GetMemberByIdAsync(id);
        if (member == null)
        {
            return NotFound();
        }
        return Ok(member);
    }

    [HttpPost]
    // Add new member and store inside SQL Database
    public async Task<IActionResult> CreateMember([FromBody] Member member)
    {
        if (ModelState.IsValid)
        {
            await _memberService.AddMemberAsync(member);
            return CreatedAtAction(nameof(GetMemberById), new { id = member.Id }, member);
        }
        return BadRequest(ModelState);
    }

    [HttpPut("{id}")]
    // Update member data  (To be implemented)
    public async Task<IActionResult> UpdateMember(Guid id, [FromBody] Member member)
    {
        if (id != member.Id)
        {
            return BadRequest();
        }

        await _memberService.UpdateMemberAsync(member);
        return NoContent();
    }

    [HttpDelete("{id}")]
    // Delete member by ID GUID
    public async Task<IActionResult> DeleteMember(Guid id)
    {
        await _memberService.DeleteMemberAsync(id);
        return NoContent();
    }
}
