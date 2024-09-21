using Microsoft.EntityFrameworkCore;
using MovieApi;
using MovieApi.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=Pizzas.db";

builder.Services.AddDbContext<MovieDb>(options => options.UseSqlite("Data Source=movies.db"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Initialize the database
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MovieDb>();
    if (db.Database.EnsureCreated())
    {
        SeedData.Initialize(db);
    }
}

app.MapGet("/movies", async (MovieDb db) => await db.Movies.ToListAsync());

app.MapGet("/movies/{id}", async (int id, MovieDb db) =>
    await db.Movies.FindAsync(id)
        is Movie movie
            ? Results.Ok(movie)
            : Results.NotFound());

app.MapPost("/movies", async (Movie newMovie, MovieDb db) =>
{
    db.Movies.Add(newMovie);
    await db.SaveChangesAsync();

    return Results.Created($"/movies/{newMovie.Id}", newMovie);
});

app.MapPut("/movies/{id}", async (int id, Movie movieUpdate, MovieDb db) =>
{
    var movieFromDb = await db.Movies.FindAsync(id);

    if (movieFromDb is null) return Results.NotFound();

    movieFromDb.Title = movieUpdate.Title;
    movieFromDb.Year = movieUpdate.Year;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/movies/{id}", async (int id, MovieDb db) =>
{
    if (await db.Movies.FindAsync(id) is Movie movie)
    {
        db.Movies.Remove(movie);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();