using Microsoft.AspNetCore.Authorization;
using pzaleszczyk.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace pzaleszczyk.Models
{

    public class Author
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Surname { get; set; }

        public string Fullname {
            get { return Name + " " + Surname; }
        }

        public ICollection<Manga> Mangas { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        [Website]
        public string Website { get; set; }

        [Display(Name = "Birthday Date")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

    }
}
