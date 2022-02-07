using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC_API.Models.ViewModels
{
    public class StudentViewModel   //json need small case letters so we copied these properties from Entity's student model and made necessary changes.
    {
        public int id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must consist characters between 2-50. (Both Inclusive)")]
        public string name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Surname must consist characters between 2-50. (Both Inclusive)")]
        public string surname { get; set; }

        [Required]
        public DateTime registerdate { get; set; } = DateTime.Now;
    }
}