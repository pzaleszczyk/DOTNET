using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [NotMapped]
        public string Fullname => Name + " " + Surname;

        public ICollection<Manga> Mangas { get; set; }

        [Website]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Website { get; set; }

        [Display(Name = "Birthday Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Birthday { get; set; }

        

    }
}
