using FOD.Common.DTOs;
using FOD.Membership.Database.Contexts;
using FOD.Membership.Database.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FODContext>(
options => options.UseSqlServer(
builder.Configuration.GetConnectionString("VODConnection")));

builder.Services.AddCors(policy => {
    policy.AddPolicy("CorsAllAccessPolicy", opt =>
    opt.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    );
});

ConfigAutoMapper();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsAllAccessPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigAutoMapper()
{
    var config = new AutoMapper.MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Director, DirectorDTO>().ReverseMap();

        cfg.CreateMap<DirectorEditDTO, Director>();
        cfg.CreateMap<DirectorCreateDTO, Director>();

        cfg.CreateMap<Genre, GenreDTO>()
        .ReverseMap()
        .ForMember(dest => dest.Films, src => src.Ignore());

        cfg.CreateMap<GenreEditDTO, GenreDTO>();
        cfg.CreateMap<GenreCreateDTO, GenreDTO>();

        cfg.CreateMap<Film, FilmDTO>()
        .ReverseMap()
        .ForMember(dest => dest.Genres, src => src.Ignore())
        .ForMember(dest => dest.Director, src => src.Ignore());

        cfg.CreateMap<FilmEditDTO, Film>()
        .ForMember(dest => dest.Genres, src => src.Ignore())
        .ForMember(dest => dest.Director, src => src.Ignore());

        cfg.CreateMap<FilmCreateDTO, Film>()
        .ForMember(dest => dest.Genres, src => src.Ignore())
        .ForMember(dest => dest.Director, src => src.Ignore());

        cfg.CreateMap<FilmGenre, FilmGenreDTO>().ReverseMap();

        cfg.CreateMap<SimilarFilm, SimilarFilmDTO>().ReverseMap();


    });
    var mapper = config.CreateMapper();
    builder.Services.AddSingleton(mapper);
}
