namespace FOD.Membership.Database.Contexts
{
    public class FODContext : DbContext
    {
        public virtual DbSet<Film> Films { get; set; } = null!;
        public virtual DbSet<SimilarFilm> SimilarFilms { get; set; } = null!;
        public virtual DbSet<Director> Directors { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<FilmGenre> FilmGenres { get; set; } = null!;
        public FODContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SimilarFilm>().HasKey(ci => new { ci.FilmId, ci.SimilarFilmId });
            modelBuilder.Entity<FilmGenre>().HasKey(ci => new { ci.FilmId, ci.GenreId });

            modelBuilder.Entity<Film>(entity =>
            {
                entity
                    .HasMany(sf => sf.SimilarFilms)
                    .WithOne(f => f.Film)
                    .HasForeignKey(sf => sf.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull);


                entity.HasMany(sf => sf.Genres)
                      .WithMany(f => f.Films)
                      .UsingEntity<FilmGenre>()
                      .ToTable("FilmGenres");
            });

            /* Seed Data */
            modelBuilder.Entity<Director>().HasData(
                new { Id = 1, Name = "Richard Schenkamn" },
                new { Id = 2, Name = "Christopher Nolan" },
                new { Id = 3, Name = "Martin Scorsese" });

            modelBuilder.Entity<Film>().HasData(
                new { Id = 1, Title = "The man from the earth", Released = DateTime.Now, Free = false, Description = "asdsadasd", FilmUrl = "/Films", DirectorId = 1 },
                new { Id = 2, Title = "Inception", Released = DateTime.Now, Free = false, Description = "asdsadasd", FilmUrl = "/Films", DirectorId = 2 },
                new { Id = 3, Title = "Shutter Island", Released = DateTime.Now, Free = false, Description = "asdsadasd", FilmUrl = "/Films", DirectorId = 3 });

            modelBuilder.Entity<SimilarFilm>().HasData(
                new SimilarFilm { FilmId = 2, SimilarFilmId = 3 });

            modelBuilder.Entity<Genre>().HasData(
                new { Id = 1, Name = "Action" },
                new { Id = 2, Name = "Sci-Fi" },
                new { Id = 3, Name = "Thriller" });


            modelBuilder.Entity<FilmGenre>().HasData(
                new FilmGenre { FilmId = 1, GenreId = 2 },
                new FilmGenre { FilmId = 2, GenreId = 1 },
                new FilmGenre { FilmId = 2, GenreId = 3 },
                new FilmGenre { FilmId = 3, GenreId = 3 });
        }
    }
}
