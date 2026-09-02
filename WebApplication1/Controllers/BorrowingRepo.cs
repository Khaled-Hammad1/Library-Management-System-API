using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowingRepo : ControllerBase
    {
        private readonly LibraryDbContext _dbContext;

        private const decimal FinePerDay = 5.00m;

        public BorrowingRepo(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpPost("BorrowItem")]
        public IActionResult BorrowItem(int memberId, int itemId, int durationDays)
        {
            var member = _dbContext.Members.Find(memberId);

            if (member == null)
            {
                return NotFound("Member not found.");
            }

            var item = _dbContext.LibraryItems.Find(itemId);

            if (item == null)
            {
                return NotFound("Item not found.");
            }

            if (item.IsAvailable == false)
            {
                return BadRequest("Item is not available for borrowing.");
            }

            if (durationDays <= 0)
            {
                return BadRequest("Duration must be greater than zero.");
            }

            DateTime borrowDate = DateTime.Now;
            DateTime dueDate = borrowDate.AddDays(durationDays);

            Borrowing borrowing = new Borrowing
            {
                MemberId = memberId,
                ItemId = itemId,
                BorrowDate = borrowDate,
                DueDate = dueDate
            };

            _dbContext.Borrowings.Add(borrowing);

            item.IsAvailable = false;

            _dbContext.SaveChanges();

            return Ok(new
            {
                Message = "Item borrowed successfully.",
                BorrowingId = borrowing.Id,
                BorrowDate = borrowDate,
                DueDate = dueDate
            });
        }


        [HttpPut("ReturnItem/{borrowingId}")]
        public IActionResult ReturnItem(int borrowingId)
        {
            var borrowing = _dbContext.Borrowings.Find(borrowingId);

            if (borrowing == null)
            {
                return NotFound("Borrowing not found.");
            }

            if (borrowing.ReturnDate != null)
            {
                return BadRequest("This item has already been returned.");
            }

            var item = _dbContext.LibraryItems.Find(borrowing.ItemId);

            if (item == null)
            {
                return NotFound("Item not found.");
            }

            DateTime returnDate = DateTime.Now;

            int lateDays = Math.Max(0,(returnDate.Date - borrowing.DueDate.Date).Days);

            decimal fine = lateDays * FinePerDay;

            borrowing.ReturnDate = returnDate;
            borrowing.Fine = fine;

            item.IsAvailable = true;

            _dbContext.SaveChanges();

            return Ok(new
            {
                Message = "Item returned successfully.",
                LateDays = lateDays,
                Fine = fine,
                ReturnDate = returnDate
            });
        }


        [HttpGet("ActiveBorrowings")]
        public IActionResult ShowActiveBorrowings()
        {
            var borrowings = _dbContext.Borrowings.Include(b => b.Member).Include(b => b.LibraryItem).Where(b => b.ReturnDate == null)
                .Select(b => new
                {
                    BorrowingId = b.Id,
                    MemberId = b.MemberId,
                    MemberName = b.Member.Name,
                    ItemId = b.ItemId,
                    ItemTitle = b.LibraryItem.Title,
                    BorrowDate = b.BorrowDate,
                    DueDate = b.DueDate
                })
                .ToList();

            return Ok(borrowings);
        }


        [HttpGet("ActiveBorrowings/{memberId}")]
        public IActionResult ShowActiveBorrowingsByID(int memberId)
        {
            var member = _dbContext.Members.Find(memberId);

            if (member == null)
            {
                return NotFound("Member not found.");
            }

            var borrowings = _dbContext.Borrowings.Include(b => b.Member).Include(b => b.LibraryItem).Where(b =>b.ReturnDate == null &&b.MemberId == memberId)
                .Select(b => new
                {
                    BorrowingId = b.Id,
                    MemberId = b.MemberId,
                    MemberName = b.Member.Name,
                    ItemId = b.ItemId,
                    ItemTitle = b.LibraryItem.Title,
                    BorrowDate = b.BorrowDate,
                    DueDate = b.DueDate
                })
                .ToList();

            return Ok(borrowings);
        }
    }
}
