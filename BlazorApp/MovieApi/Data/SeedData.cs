namespace MovieApi.Data;

public static class SeedData
{
    public static void Initialize(MovieDb db)
    {
        var movies = new Movie[]
        {
            new Movie
            {
                Id = 1,
                Title = "Forrest Gump",
                Year = 2020,
                PosterUri = "https://en.wikipedia.org/wiki/Forrest_Gump#/media/File:Forrest_Gump_poster.jpg"
            },
            new Movie
            {
                Id = 2,
                Title = "Dodge Ball",
                Year = 2016,
                PosterUri = "https://en.wikipedia.org/wiki/Dodgeball:_A_True_Underdog_Story#/media/File:Movie_poster_Dodgeball_A_True_Underdog_Story.jpg"
            },
        };
        
        db.Movies.AddRange(movies);
        db.SaveChanges();
    }
}