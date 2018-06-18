using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Warsztaty_3.Models
{
    public class PersonModel
    {
        [Key]
        [DisplayName("ID")]
        [Required]
        public int ID { get; set; }

        [Required]
        [DisplayName("Imię")]
        [MinLength(3)]
        [MaxLength(255)]
        public string FirstName { get; set; }
        
        [DisplayName("Nazwisko")]
        [MinLength(3)]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Numer telefonu")]
        [MinLength(4)]
        [MaxLength(16)]
        public string Phone { get; set; }

        [DisplayName("Adres Email")]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [DisplayName("Data dodania")]
        public DateTime Created { get; set; }

        [Required]
        [DisplayName("Data zmodyfikowania")]
        public DateTime Updated { get; set; }

        public PersonModel()
        {
            var now = DateTime.Now;
            Created = now;
            Updated = now;
        }


    }
}
