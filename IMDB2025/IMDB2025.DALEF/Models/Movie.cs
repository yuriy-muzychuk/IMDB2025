using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IMDB2025.DALEF.Models;

public partial class Movie
{
    [Key]
    public int MovieId { get; set; }

    [StringLength(200)]
    public string Title { get; set; } = null!;

    public DateOnly? ReleaseDate { get; set; }

    public int GenreId { get; set; }

    [InverseProperty("Movie")]
    public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();

    [ForeignKey("GenreId")]
    [InverseProperty("Movies")]
    public virtual Genre Genre { get; set; } = null!;

    [ForeignKey("MovieId")]
    [InverseProperty("Movies")]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
