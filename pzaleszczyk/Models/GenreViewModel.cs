using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pzaleszczyk.Models
{
    
    public class GenreViewModel
    {
        public List<Manga> Mangas { get; set; }
        public SelectList Genres { get; set; }
        public string MangaGenre { get; set; }
        public string SearchString { get; set; }
    }
}
