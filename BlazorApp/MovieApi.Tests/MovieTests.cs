namespace MovieApi.Tests;

public class MovieTests
{
    [Fact]
    public void BuildPrintableMovieTitleWithYearLessThanZero()
    {
        // Arrange
        var movie = new Movie
            { Id = 1, Title = "My Test Movie", PosterUri = "https://imbd.com/graphic/png/10002.png", Year = 2002 };

        // Act
        var printableMovieTitle = movie.BuildPrintableMovieTitle();

        // Assert
        Assert.Equal("Title:My Test Movie Year:2002", printableMovieTitle);
    }
}