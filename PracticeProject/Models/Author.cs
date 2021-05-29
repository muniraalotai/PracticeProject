using System.ComponentModel.DataAnnotations;

namespace PracticeProject.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string FullName { get; set; }
    }
}