using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberRepo : ControllerBase
    {
        private readonly LibraryDbContext _dbContext;

        public MemberRepo(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("ListMember")]
        public ActionResult<IEnumerable<Member>> ListMember()
        {
                return Ok(_dbContext.Members.ToList());
            
        }

        [HttpPost("AddMember")]
        public IActionResult AddMember(Member member)
        {
            _dbContext.Members.Add(member);
            _dbContext.SaveChanges();
            return Ok("Member added successfully.");
        }

        [HttpPut("UpdateMember/{id}")]
        public IActionResult UpdateMember(int id, Member member)
        {
            var oldMember = _dbContext.Members
                .FirstOrDefault(m => m.Id == id);

            if (oldMember == null)
            {
                return NotFound("Member not found.");
            }

            oldMember.Name = member.Name;
            oldMember.Email = member.Email;
            oldMember.PhoneNumber = member.PhoneNumber;
            oldMember.MembershipId = member.MembershipId;
            oldMember.RoleOfMem = member.RoleOfMem;

            _dbContext.SaveChanges();

            return Ok("Member updated successfully.");
        }

        [HttpDelete("DeleteMember/{id}")]
        public IActionResult DeleteMember(int id)
        {
            var mem = _dbContext.Members.FirstOrDefault(b => b.Id == id);
            if (mem == null)
            {
                return NotFound("Member not found.");
            }
            _dbContext.Members.Remove(mem);
            _dbContext.SaveChanges();
            return Ok("Member Deleted successfully.");
        }

        [HttpGet("ShowBorrowingHistory/{memberId}")]
        public IActionResult ShowBorrowingHistory(int memberId)
        {
            var temp = _dbContext.Borrowings.Include(b => b.LibraryItem).Where(b => b.MemberId == memberId).ToList();
            return Ok(temp);

        }

        [HttpGet("GetMemberByEmail/{email}")]
        public IActionResult GetMemberByEmail(String email)
        {
            var temp = _dbContext.Members.FirstOrDefault(b => b.Email == email);
            if (temp == null)
            {
                return NotFound("The member is not found");
            }
            return Ok(temp);
           
        }


    }
}

