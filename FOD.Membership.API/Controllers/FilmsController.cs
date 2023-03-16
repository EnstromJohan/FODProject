namespace FOD.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IDbService _db;

        public FilmsController(IDbService db)
        {
            _db = db;
        }

        
        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                _db.Include<Film>();
                var film = await _db.GetAsync<Film, FilmDTO>();
                return Results.Ok(film);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        }

       
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            try
            {
                _db.Include<Film>();
                _db.Include<Genre>();
                var film = await _db.SingleAsync<Film, FilmDTO>(f => f.Id == id);
                return Results.Ok(film);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        }

        
        [HttpPost]
        public async Task<IResult> Post([FromBody] FilmCreateDTO dto)
        {

            try
            {
                var film = await _db.AddAsync<Film, FilmCreateDTO>(dto);
                var result = await _db.SaveChangesAsync();

                if (!result) return Results.BadRequest();

                return Results.Created(_db.GetURI(film), film);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] FilmEditDTO dto)
        {


            try
            {
                if (id != dto.Id) return Results.BadRequest("Id no match");
                var exists = await _db.AnyAsync<Director>(f => f.Id == dto.DirectorId);
                if (!exists) return Results.NotFound("Director not found");

                exists = await _db.AnyAsync<Film>(f => f.Id == id);
                if (!exists) return Results.NotFound("Film not found");

                _db.Update<Film, FilmEditDTO>(id, dto);
                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();

                return Results.NoContent();

            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                var exists = await _db.AnyAsync<Film>(f => f.Id == id);
                if (!exists) return Results.NotFound("Film not found");

                var success = await _db.DeleteAsync<Film>(id);
                if (!success) return Results.NotFound("Film not found");

                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();

                return Results.NoContent();

            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
