using GraphQL_API.Data;
using GraphQL_API.Models;

namespace GraphQL_API.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(AppDbContext))]
        [UseProjection]
        public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context) => context.Platforms;
    }
}