using Microsoft.EntityFrameworkCore;

namespace MovieApi.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var context = new MovieDb(serviceProvider.GetRequiredService<DbContextOptions<MovieDb>>());

        context.Add(new Movie
            {
                Id = 1,
                Title = "Forrest Gump",
                Year = 2020,
                PosterUri = "https://en.wikipedia.org/wiki/Forrest_Gump#/media/File:Forrest_Gump_poster.jpg"
            });
        
        context.Add(new Movie
        {
            Id = 2,
            Title = "Dodge Ball",
            Year = 2016,
            PosterUri = "https://en.wikipedia.org/wiki/Dodgeball:_A_True_Underdog_Story#/media/File:Movie_poster_Dodgeball_A_True_Underdog_Story.jpg"
        });

        await context.SaveChangesAsync();
    }
}