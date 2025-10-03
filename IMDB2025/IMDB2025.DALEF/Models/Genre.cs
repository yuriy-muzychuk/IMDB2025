using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IMDB2025.DALEF.Models;

public partial class Genre
{
    [Key]
    public int GenreId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("Genre")]
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
