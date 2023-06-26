using Domain.Common.Models;
using Domain.Users.ValueObjects;

namespace Domain.Users.Entity;

public class MovieList : Entity<MovieListId>
{
    public string Name { get; private set; }
    private readonly List<TmdbID> _movieIds = new();
    public int ListMinutes { get; private set; } = 0;
    public IReadOnlyList<TmdbID> MovieIds => _movieIds.AsReadOnly();
        
    public string OwnerId { get; private set; }
    private List<string> _managerIds = new();
    public IReadOnlyList<string> ManagerIds=> _managerIds.AsReadOnly();

    private MovieList(MovieListId id, string ownerId, string name) : base(id)
    {
        OwnerId = ownerId;
        Name = name;
    }
    public static MovieList Create(string ownerId, string name)
    {
        return new(
            MovieListId.CreateUnique(),
            ownerId,
            name
           );
    }

    public void AddMovie(int movieId, int runTime)
    {
        _movieIds.Add(TmdbID.Create(movieId));
        ListMinutes += runTime;
    }

    public void RemoveMovie(int movieId, int runTime)
    {
        _movieIds.Remove(TmdbID.Create(movieId));
        ListMinutes -= runTime;
    }

    public void AddManger(string managerId)
    {
        _managerIds.Add(managerId);
    }

    public void RemoveManager(string managerId)
    {
        _managerIds.Remove(managerId);
    }

    private MovieList() { }
}