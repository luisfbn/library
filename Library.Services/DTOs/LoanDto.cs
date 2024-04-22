namespace Library.Services.DTOs
{
    public class LoanDto
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string BookTitle { get; set; }

        public int ReaderId { get; set; }

        public string ReaderName { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }

}
