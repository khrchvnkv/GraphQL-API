using GraphQL_API.Data;
using GraphQL_API.GraphQL;
using GraphQL_API.GraphQL.Commands;
using GraphQL_API.GraphQL.Platforms;
using GraphQL.Server.Ui.Voyager;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddType<PlatformType>()
    .AddType<CommandType>()
    .AddFiltering()
    .AddSorting()
    .AddInMemorySubscriptions();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseWebSockets();
app.UseRouting();

app.UseEndpoints(endpoints => endpoints.MapGraphQL());
app.UseGraphQLVoyager("/graphql-voyager", new VoyagerOptions());

app.Run();