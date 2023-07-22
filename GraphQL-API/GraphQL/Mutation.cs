using GraphQL_API.Data;
using GraphQL_API.GraphQL.Commands;
using GraphQL_API.GraphQL.Platforms;
using GraphQL_API.Models;

namespace GraphQL_API.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input, 
            [ScopedService] AppDbContext context)
        {
            var platform = new Platform()
            {
                Name = input.Name,
                LicenseKey = input.LicenseKey
            };

            await context.Platforms.AddAsync(platform);
            await context.SaveChangesAsync();

            return new AddPlatformPayload(platform);
        }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input,
            [ScopedService] AppDbContext context)
        {
            var command = new Command()
            {
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId
            };

            await context.Commands.AddAsync(command);
            await context.SaveChangesAsync();

            return new AddCommandPayload(command);
        }
    }
}