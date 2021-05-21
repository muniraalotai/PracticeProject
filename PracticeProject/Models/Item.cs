using System;
using System.ComponentModel.DataAnnotations;

namespace PracticeProject.Models
{
    public class Item
    {
        public int Id { get; set; }
        [StringLength(20, ErrorMessage = "name length should be equal to 20")]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Due { get; set; }
        
    }
}