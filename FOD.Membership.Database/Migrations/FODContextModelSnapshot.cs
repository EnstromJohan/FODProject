﻿// <auto-generated />
using System;
using FOD.Membership.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FOD.Membership.Database.Migrations
{
    [DbContext(typeof(FODContext))]
    partial class FODContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FOD.Membership.Database.Entities.Director", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Directors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Richard Schenkamn"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Christopher Nolan"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Martin Scorsese"
                        });
                });

            modelBuilder.Entity("FOD.Membership.Database.Entities.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("DirectorId")
                        .HasColumnType("int");

                    b.Property<string>("FilmUrl")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<bool>("Free")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Released")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("DirectorId");

                    b.ToTable("Films");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "asdsadasd",
                            DirectorId = 1,
                            FilmUrl = "/Films",
                            Free = false,
                            Released = new DateTime(2023, 3, 6, 17, 19, 12, 233, DateTimeKind.Local).AddTicks(3083),
                            Title = "The man from the earth"
                        },
                        new
                        {
                            Id = 2,
                            Description = "asdsadasd",
                            DirectorId = 2,
                            FilmUrl = "/Films",
                            Free = false,
                            Released = new DateTime(2023, 3, 6, 17, 19, 12, 233, DateTimeKind.Local).AddTicks(3121),
                            Title = "Inception"
                        },
                        new
                        {
                            Id = 3,
                            Description = "asdsadasd",
                            DirectorId = 3,
                            FilmUrl = "/Films",
                            Free = false,
                            Released = new DateTime(2023, 3, 6, 17, 19, 12, 233, DateTimeKind.Local).AddTicks(3124),
                            Title = "Shutter Island"
                        });
                });

            modelBuilder.Entity("FOD.Membership.Database.Entities.FilmGenre", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("FilmId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("FilmGenres", (string)null);

                    b.HasData(
                        new
                        {
                            FilmId = 1,
                            GenreId = 2
                        },
                        new
                        {
                            FilmId = 2,
                            GenreId = 1
                        },
                        new
                        {
                            FilmId = 2,
                            GenreId = 3
                        },
                        new
                        {
                            FilmId = 3,
                            GenreId = 3
                        });
                });

            modelBuilder.Entity("FOD.Membership.Database.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Sci-Fi"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Thriller"
                        });
                });

            modelBuilder.Entity("FOD.Membership.Database.Entities.SimilarFilm", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.Property<int>("SimilarFilmId")
                        .HasColumnType("int");

                    b.HasKey("FilmId", "SimilarFilmId");

                    b.HasIndex("SimilarFilmId");

                    b.ToTable("SimilarFilms");

                    b.HasData(
                        new
                        {
                            FilmId = 2,
                            SimilarFilmId = 3
                        });
                });

            modelBuilder.Entity("FOD.Membership.Database.Entities.Film", b =>
                {
                    b.HasOne("FOD.Membership.Database.Entities.Director", "Director")
                        .WithMany()
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Director");
                });

            modelBuilder.Entity("FOD.Membership.Database.Entities.FilmGenre", b =>
                {
                    b.HasOne("FOD.Membership.Database.Entities.Film", null)
                        .WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FOD.Membership.Database.Entities.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FOD.Membership.Database.Entities.SimilarFilm", b =>
                {
                    b.HasOne("FOD.Membership.Database.Entities.Film", "Film")
                        .WithMany("SimilarFilms")
                        .HasForeignKey("FilmId")
                        .IsRequired();

                    b.HasOne("FOD.Membership.Database.Entities.Film", "Similar")
                        .WithMany()
                        .HasForeignKey("SimilarFilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Similar");
                });

            modelBuilder.Entity("FOD.Membership.Database.Entities.Film", b =>
                {
                    b.Navigation("SimilarFilms");
                });
#pragma warning restore 612, 618
        }
    }
}
