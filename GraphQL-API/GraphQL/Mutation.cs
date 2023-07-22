using GraphQL_API.Data;
using GraphQL_API.GraphQL.Commands;
using GraphQL_API.GraphQL.Platforms;
using GraphQL_API.Models;
using HotChocolate.Subscriptions;

namespace GraphQL_API.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(
            AddPlatformInput input, 
            [ScopedService] AppDbContext context, 
            [Service] ITopicEventSender sender,
            CancellationToken cancellationToken)
        {
            var platform = new Platform()
            {
                Name = input.Name,
                LicenseKey = input.LicenseKey
            };

            await context.Platforms.AddAsync(platform, cancellationToken: cancellationToken);
            await context.SaveChangesAsync(cancellationToken: cancellationToken);

            await sender.SendAsync(nameof(Subscription.OnPlatformAdded), platform,
                cancellationToken: cancellationToken);

            return new AddPlatformPayload(platform);
        }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(
            AddCommandInput input,
            [ScopedService] AppDbContext context,
            [Service] ITopicEventSender sender,
            CancellationToken cancellationToken)
        {
            var command = new Command()
            {
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId
            };

            await context.Commands.AddAsync(command, cancellationToken: cancellationToken);
            await context.SaveChangesAsync(cancellationToken: cancellationToken);

            await sender.SendAsync(nameof(Subscription.OnCommandAdded), command, cancellationToken);

            return new AddCommandPayload(command);
        }
    }
}