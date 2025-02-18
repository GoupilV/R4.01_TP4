using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using R4._01_TP4.Models.EntityFramework;

namespace R4._01_TP4.Models.EntityFramework;

public partial class FilmsRatingDBContext : DbContext
{
    public FilmsRatingDBContext()
    {
    }

    public FilmsRatingDBContext(DbContextOptions<FilmsRatingDBContext> options)
        : base(options)
    {
    }

    public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

    public virtual DbSet<Notation> Notations{ get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs{ get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLoggerFactory(MyLoggerFactory)
        .EnableSensitiveDataLogging()
        .UseNpgsql("Server=localhost; port=5432;Database=FilmsRatingDB; uid=postgres; password=postgres;");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Migration : dotnet-ef migrations add CreationBDFilmRatings --project R4.01_TP4
        modelBuilder.HasDefaultSchema("public");

        modelBuilder.Entity<Notation>(entity =>
        {
            entity.HasKey(e => new { e.FilmId, e.UtilisateurId }).HasName("pk_notation");

            entity.HasOne(d => d.FilmNote).WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_notation_film");

            entity.HasOne(d => d.FilmNote).WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_notation_utilisateur");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.Idfilm).HasName("pk_film");

            entity.Property(e => e.DateSortie)
                 .HasColumnType("date");

            entity.HasMany(d => d.NotesFilm)
                .WithOne(p => p.FilmNote)
                .HasForeignKey(d => d.FilmId )
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_film_notation");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.UtilisateurId).HasName("pk_utilisateur");

            entity.Property(e => e.CodePostal)
                .HasColumnType("char");

            entity.Property(e => e.Pays)
                .HasDefaultValue("France");

            entity.Property(e => e.DateCreation)
                .HasColumnType("date");

            entity.HasIndex(e => e.Mail)
                .IsUnique();

            entity.HasMany(d => d.NotesUtilisateur)
                .WithOne(p => p.UtilisateurNote)
                .HasForeignKey(d => d.UtilisateurId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_utilisateur_notation");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
