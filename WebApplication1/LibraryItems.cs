namespace WebApplication1
{
    public class LibraryItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? AuthorPublisher { get; set; }

        public int? YearOfPublication { get; set; }

        public bool? IsAvailable { get; set; }

        public string ItemType { get; set; }

        public string? ISBN { get; set; }

        public int? NumberOfPages { get; set; }

        public int? IssueNumber { get; set; }

        public string? Category { get; set; }

        public DateTime? PublicationDate { get; set; }

        public string? Region { get; set; }

        public LibraryItem()
        {
        }

        public LibraryItem(
            string title,
            string? authorPublisher,
            int? yearOfPublication,
            bool? isAvailable,
            string itemType,
            string? iSBN,
            int? numberOfPages,
            int? issueNumber,
            string? category,
            DateTime? publicationDate,
            string? region)
        {
            Title = title;
            AuthorPublisher = authorPublisher;
            YearOfPublication = yearOfPublication;
            IsAvailable = isAvailable;
            ItemType = itemType;
            ISBN = iSBN;
            NumberOfPages = numberOfPages;
            IssueNumber = issueNumber;
            Category = category;
            PublicationDate = publicationDate;
            Region = region;
        }
    }
}
