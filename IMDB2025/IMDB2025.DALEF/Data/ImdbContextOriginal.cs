using System;
using System.Collections.Generic;
using IMDB2025.DALEF.Models;
using Microsoft.EntityFrameworkCore;

namespace IMDB2025.DALEF.Data;

public partial class ImdbContextOriginal : DbContext
{
    public ImdbContextOriginal()
    {
    }

    public ImdbContextOriginal(DbContextOptions<ImdbContextOriginal> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("abracadabra");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasOne(d => d.Movie).WithMany(p => p.Actors).HasConstraintName("FK_Actors_Movies");

            entity.HasOne(d => d.Person).WithMany(p => p.Actors).HasConstraintName("FK_Actors_Persons");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasOne(d => d.Genre).WithMany(p => p.Movies).HasConstraintName("FK_Movies_Genres");

            entity.HasMany(d => d.People).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "Director",
                    r => r.HasOne<Person>().WithMany()
                        .HasForeignKey("PersonId")
                        .HasConstraintName("FK_Directors_Persons"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .HasConstraintName("FK_Directors_Movies"),
                    j =>
                    {
                        j.HasKey("MovieId", "PersonId");
                        j.ToTable("Directors");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
