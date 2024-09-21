namespace BlazorApp;

public class MoviesHttpClient(HttpClient client)
{
    public async Task<MovieDto[]> GetTodoItemsAsync()
    {
        return await client.GetFromJsonAsync<MovieDto[]>("movies") ?? [];
    }
}
