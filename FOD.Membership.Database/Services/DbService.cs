using System.Linq.Expressions;
using FOD.Membership.Database.Contexts;

namespace FOD.Membership.Database.Services
{
    public class DbService
    {
        private readonly FODContext _db;
        private readonly IMapper _mapper;

        public DbService(FODContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<TDto>> GetAsync<TEntity, TDto>()
           where TEntity : class, IEntity
           where TDto : class
        {
            var entities = await _db.Set<TEntity>().ToListAsync();
            return _mapper.Map<List<TDto>>(entities);
        }

        public async Task<List<TDto>> GetAsync<TEntity, TDto>(
            Expression<Func<TEntity, bool>> expression)
            where TEntity : class, IEntity
            where TDto : class
        {
            var entitites = await _db.Set<TEntity>()
                .Where(expression)
                .ToListAsync();
            return _mapper.Map<List<TDto>>(entitites);

        }


    }
}
