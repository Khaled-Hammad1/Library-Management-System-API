using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1
{
    public class Borrowing
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }

        [ForeignKey(nameof(LibraryItem))]
        public int ItemId { get; set; }


        public LibraryItem? LibraryItem { get; set; }
        public Member? Member { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal Fine { get; set; }

    }
}
