using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetMVC_API_Entity.Models   //Class Library (.net framwork)
{
    [Table("Students")]
    public class Student
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must consist characters between 2-50. (Both Inclusive)")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Surname must consist characters between 2-50. (Both Inclusive)")]
        public string Surname { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; } = DateTime.Now;
    }
}
