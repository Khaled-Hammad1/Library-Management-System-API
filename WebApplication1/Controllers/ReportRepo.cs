using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportRepo : ControllerBase
    {
        private readonly LibraryDbContext _dbContext;

        public ReportRepo(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("MostBorrowedItem")]
        public IActionResult MostBorrowedItem()
        {
            var result = _dbContext.Borrowings
                .GroupBy(b => new
                {
                    b.ItemId,
                    b.LibraryItem.Title,
                    b.LibraryItem.ItemType
                })
                .Select(g => new
                {
                    Id = g.Key.ItemId,
                    Title = g.Key.Title,
                    ItemType = g.Key.ItemType,
                    BorrowCount = g.Count()
                })
                .OrderByDescending(x => x.BorrowCount)
                .ToList();

            if (result.Count == 0)
            {
                return Ok(result);
            }

            int maxBorrowCount = result[0].BorrowCount;

            var mostBorrowed = result.Where(x => x.BorrowCount == maxBorrowCount).ToList();
            return Ok(mostBorrowed);
        }


        [HttpGet("TotalFines")]
        public IActionResult TotalFines()
        {
            decimal totalFines = _dbContext.Borrowings.Sum(b => b.Fine);

            return Ok(new
            {
                TotalFines = totalFines
            });
        }


        [HttpGet("MembersWithMostBorrowings")]
        public IActionResult MembersWithMostBorrowings()
        {
            var result = _dbContext.Borrowings
                .GroupBy(b => new
                {
                    b.MemberId,
                    b.Member.Name,
                    b.Member.MembershipId
                })
                .Select(g => new
                {
                    Id = g.Key.MemberId,
                    Name = g.Key.Name,
                    MembershipId = g.Key.MembershipId,
                    BorrowCount = g.Count()
                })
                .OrderByDescending(x => x.BorrowCount)
                .ToList();

            if (result.Count == 0)
            {
                return Ok(result);
            }

            int maxBorrowCount = result[0].BorrowCount;

            var members = result.Where(x => x.BorrowCount == maxBorrowCount).ToList();

            return Ok(members);
        }


        [HttpGet("PopularItems")]
        public IActionResult PopularItems()
        {
            var result = _dbContext.Borrowings
                .GroupBy(b => new
                {
                    b.ItemId,
                    b.LibraryItem.Title,
                    b.LibraryItem.ItemType
                })
                .Select(g => new
                {
                    Id = g.Key.ItemId,
                    Title = g.Key.Title,
                    ItemType = g.Key.ItemType,
                    BorrowCount = g.Count()
                })
                .OrderByDescending(x => x.BorrowCount)
                .Take(5)
                .ToList();

            return Ok(result);
        }


        [HttpGet("BorrowedItemsPerType")]
        public IActionResult BorrowedItemsPerType()
        {
            var result = _dbContext.Borrowings
                .GroupBy(b => b.LibraryItem.ItemType)
                .Select(g => new
                {
                    ItemType = g.Key,
                    TotalBorrowed = g.Count()
                })
                .ToList();

            return Ok(result);
        }


        [HttpGet("FinesOverTime")]
        public IActionResult FinesOverTime()
        {
            var result = _dbContext.Borrowings
                .Where(b => b.ReturnDate != null && b.Fine > 0)
                .GroupBy(b => b.ReturnDate!.Value.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    TotalFine = g.Sum(b => b.Fine)
                })
                .OrderBy(x => x.Date)
                .ToList();

            return Ok(result);
        }
    }
}
