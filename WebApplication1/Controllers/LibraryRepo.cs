using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryRepo : ControllerBase
    {
        private readonly LibraryDbContext _dbContext;

        public LibraryRepo(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("AddItem")]
        public IActionResult AddItem(LibraryItem item)
        {
            _dbContext.LibraryItems.Add(item);
            _dbContext.SaveChanges();

            return Ok("Item added successfully.");
        }


        [HttpPut("UpdateItem/{id}")]
        public IActionResult UpdateItem(int id, LibraryItem item)
        {
            var oldItem = _dbContext.LibraryItems.FirstOrDefault(i => i.Id == id);

            if (oldItem == null)
            {
                return NotFound("Item not found.");
            }

            oldItem.Title = item.Title;
            oldItem.AuthorPublisher = item.AuthorPublisher;
            oldItem.YearOfPublication = item.YearOfPublication;
            oldItem.IsAvailable = item.IsAvailable;
            oldItem.ItemType = item.ItemType;

            _dbContext.SaveChanges();

            return Ok("Item updated successfully.");
        }


        [HttpDelete("DeleteItem/{id}")]
        public IActionResult RemoveItem(int id)
        {
            var item = _dbContext.LibraryItems.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                return NotFound("Item not found.");
            }

            _dbContext.LibraryItems.Remove(item);
            _dbContext.SaveChanges();

            return Ok("Item deleted successfully.");
        }


        [HttpGet("AvailableItems")]
        public IActionResult ListAvailableItems()
        {
            var items = _dbContext.LibraryItems.Where(i => i.IsAvailable == true).ToList();
            return Ok(items);
        }

        [HttpGet("SearchByYear/{year}")]
        public IActionResult SearchItemsByYear(int year)
        {
            var items = _dbContext.LibraryItems.Where(i => i.YearOfPublication == year).ToList();

            if (items.Count == 0)
            {
                return NotFound("No items found.");
            }

            return Ok(items);
        }


        [HttpGet("SearchByTitle")]
        public IActionResult SearchItemsByTitle(string search)
        {
            var items = _dbContext.LibraryItems.Where(i => i.Title == search).ToList();

            if (items.Count == 0)
            {
                return NotFound("No items found.");
            }

            return Ok(items);
        }


        [HttpGet("SearchByAuthor")]
        public IActionResult SearchItemsByAuthor(string search)
        {
            var items = _dbContext.LibraryItems.Where(i => i.AuthorPublisher == search).ToList();

            if (items.Count == 0)
            {
                return NotFound("No items found.");
            }
            return Ok(items);
        }


        [HttpGet("FilterByType/{type}")]
        public IActionResult FilterByType(string type)
        {
            var items = _dbContext.LibraryItems.Where(i => i.ItemType == type).ToList();

            if (items.Count == 0)
            {
                return NotFound("No items found.");
            }
            return Ok(items);
        }
    }
}
