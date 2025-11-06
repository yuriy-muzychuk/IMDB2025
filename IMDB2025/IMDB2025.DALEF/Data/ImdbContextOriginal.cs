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

    public virtual DbSet<Privilege> Privileges { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPrivilege> UserPrivileges { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=C3PO;Initial Catalog=IMDB2025;Integrated Security=True;TrustServerCertificate=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Ukrainian_CI_AS");

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

        modelBuilder.Entity<Privilege>(entity =>
        {
            entity.Property(e => e.RowInsertTime).HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Password).IsFixedLength();
            entity.Property(e => e.RowInsertTime).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.RowUpdateTime).HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<UserPrivilege>(entity =>
        {
            entity.Property(e => e.RowInsertTime).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.Privilege).WithMany(p => p.UserPrivileges)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPrivileges_Privileges");

            entity.HasOne(d => d.User).WithMany(p => p.UserPrivileges)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPrivileges_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
