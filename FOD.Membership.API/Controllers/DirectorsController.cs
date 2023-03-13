namespace FOD.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly IDbService _db;

        public DirectorsController(IDbService db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {

                var director = await _db.GetAsync<Director, DirectorDTO>();
                return Results.Ok(director);
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
                _db.Include<Director>();
                var director = await _db.SingleAsync<Director, DirectorDTO>(d => d.Id == id);
                return Results.Ok(director);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IResult> Post([FromBody] DirectorCreateDTO dto)
        {

            try
            {
                var director = await _db.AddAsync<Director, DirectorCreateDTO>(dto);
                var result = await _db.SaveChangesAsync();

                if (!result) return Results.BadRequest();

                return Results.Created(_db.GetURI(director), director);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] DirectorEditDTO dto)
        {


            try
            {
                if (id != dto.Id) return Results.BadRequest("Id no match");
                var exists = await _db.AnyAsync<Director>(d => d.Id == dto.Id);
                if (!exists) return Results.NotFound("Director not found");

                _db.Update<Director, DirectorEditDTO>(id, dto);
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
                var exists = await _db.AnyAsync<Director>(d => d.Id == id);
                if (!exists) return Results.NotFound("Director not found");

                var success = await _db.DeleteAsync<Director>(id);
                if (!success) return Results.NotFound("Director not found");

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
