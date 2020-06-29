using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCVidly.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public byte GenreId { get; set; }
       
        public DateTime? DateAdded { get; set; }

    
        public DateTime? ReleaseDate { get; set; }

      
        public byte NumberInStock { get; set; }
    }
}