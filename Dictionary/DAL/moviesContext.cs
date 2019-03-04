using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Dictionary
{
    public partial class moviesContext : DbContext
    {
        public moviesContext()
        {
        }

        public moviesContext(DbContextOptions<moviesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actors> Actors { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<MoviesActors> MoviesActors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=movies;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("cube")
                .HasPostgresExtension("dict_xsyn")
                .HasPostgresExtension("fuzzystrmatch")
                .HasPostgresExtension("pg_trgm")
                .HasPostgresExtension("pgcrypto")
                .HasPostgresExtension("tablefunc")
                .HasAnnotation("ProductVersion", "3.0.0-preview.19074.3");

            modelBuilder.Entity<Actors>(entity =>
            {
                entity.HasKey(e => e.ActorId)
                    .HasName("actors_pkey");

                entity.ToTable("actors");

                entity.Property(e => e.ActorId).HasColumnName("actor_id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("genres_pkey");

                entity.ToTable("genres");

                entity.HasIndex(e => e.Name)
                    .HasName("genres_name_key")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .ValueGeneratedNever();

                entity.Property(e => e.Position).HasColumnName("position");
            });

            modelBuilder.Entity<Movies>(entity =>
            {
                entity.HasKey(e => e.MovieId)
                    .HasName("movies_pkey");

                entity.ToTable("movies");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.Title).HasColumnName("title");
            });

            modelBuilder.Entity<MoviesActors>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.ActorId })
                    .HasName("my_table_pkey");

                entity.ToTable("movies_actors");

                entity.HasIndex(e => e.ActorId)
                    .HasName("movies_actors_actor_id");

                entity.HasIndex(e => e.MovieId)
                    .HasName("movies_actors_movie_id");

                entity.HasIndex(e => new { e.MovieId, e.ActorId })
                    .HasName("movies_actors_movie_id_actor_id_key")
                    .IsUnique();

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.ActorId).HasColumnName("actor_id");

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.MoviesActors)
                    .HasForeignKey(d => d.ActorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("movies_actors_actor_id_fkey");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MoviesActors)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("movies_actors_movie_id_fkey");
            });
        }
    }
}
