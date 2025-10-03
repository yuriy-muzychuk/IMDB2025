using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IMDB2025.DALEF.Models;

[PrimaryKey("MovieId", "PersonId", "CharacterName")]
public partial class Actor
{
    [Key]
    public int MovieId { get; set; }

    [Key]
    public int PersonId { get; set; }

    [Key]
    [StringLength(50)]
    public string CharacterName { get; set; } = null!;

    [ForeignKey("MovieId")]
    [InverseProperty("Actors")]
    public virtual Movie Movie { get; set; } = null!;

    [ForeignKey("PersonId")]
    [InverseProperty("Actors")]
    public virtual Person Person { get; set; } = null!;
}
