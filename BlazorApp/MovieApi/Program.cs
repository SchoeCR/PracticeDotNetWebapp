using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using MovieApi;
using MovieApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MovieDb>(options => options.UseSqlite("Data Source=movies.db"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container
builder.Services.AddEndpointsApiExplorer();

// Add NSwag services
builder.Services.AddOpenApiDocument();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    // Seed the database
    await using var scope = app.Services.CreateAsyncScope();
    await SeedData.InitializeAsync(scope.ServiceProvider);

    // Add OpenAPI/Swagger generator and the Swagger UI
    app.UseOpenApi();
    app.UseSwaggerUi();
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