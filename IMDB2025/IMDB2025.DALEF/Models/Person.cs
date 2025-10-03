using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IMDB2025.DALEF.Models;

public partial class Person
{
    [Key]
    public int PersonId { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string? LastName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public bool IsFemale { get; set; }

    [InverseProperty("Person")]
    public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();

    [ForeignKey("PersonId")]
    [InverseProperty("People")]
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
