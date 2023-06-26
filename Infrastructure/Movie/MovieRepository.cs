using Application.Common.Interfaces;
using Application.StreamedContentList.Command.AddMovie;
using Application.StreamedContentList.Common;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Movie;

public class MovieRepository : IMovieRepository
{
    private readonly IHttpClientFactory _httpClientFactory;

    public MovieRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<MovieResponse> GetMovieDetailsAsync(int id)
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");
        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJlNDliNDVhYTkxN2Q2MTRhYmY3ZTAzNDM3YjM3ZWZhNCIsInN1YiI6IjY0OTAxODNhMjYzNDYyMDEwY2JjZDJjNCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.OnTuyzUydk1TfAnireWGQHiTDeUVdT_-1b9Y5i8qG7c");

        HttpResponseMessage response = await httpClient.GetAsync($"movie/{id}");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            MovieDetails? movieDetails = JsonSerializer.Deserialize<MovieDetails>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            MovieResponse mr = new MovieResponse
            (   movieDetails.Id,
                movieDetails.Title,
                movieDetails.Overview,
                movieDetails.Runtime
            );
            return mr;
        }

        throw new Exception("Error while finding movie");
    }
}
