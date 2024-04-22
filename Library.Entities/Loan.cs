using System.ComponentModel.DataAnnotations;

namespace Library.Entities
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public int ReaderId { get; set; }

        public Reader Reader { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }

}
