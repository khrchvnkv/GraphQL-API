using GraphQL_API.Data;
using GraphQL_API.GraphQL;
using GraphQL.Server.Ui.Voyager;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddProjections();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoints => endpoints.MapGraphQL());
app.UseGraphQLVoyager("/graphql-voyager", new VoyagerOptions());

app.Run();