namespace MovieApi
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string PosterUri { get; set; }

        public string BuildPrintableMovieTitle()
        {
            string printableMovieTitle;

            if (Year <= 0)
            {
                printableMovieTitle = $"Title:{Title}";    
            }
            else
            {
                printableMovieTitle = $"Title:{Title} Year:{Year}";
            }
            
            return printableMovieTitle;
        }
    }
}
