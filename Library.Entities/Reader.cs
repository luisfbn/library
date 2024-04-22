using System.ComponentModel.DataAnnotations;

namespace Library.Entities
{
    public class Reader
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Loan> Loans { get; set; }
    }
}
