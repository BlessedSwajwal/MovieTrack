using Domain.Common.Models;
using Domain.Users.Entity;
using Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users;

public sealed class User : AggregateRoot<UserId, Guid>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public bool EmailVerified { get; private set; } = false;
    public StreamedListId streamedListId { get; private set; }

    private List<MovieListId> _movieListIds= new List<MovieListId>();
    public IReadOnlyList<MovieListId> MovieListIds => _movieListIds;

    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    # region Private Constructor 
    private User(
        UserId id,
        string firstName,
        string lastName,
        string email,
        string password,
        DateTime createdDateTime,
        DateTime updatedDateTime,
        StreamedListId streamedListId)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
        this.streamedListId = streamedListId;
    }

    #endregion

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string password,
        StreamedList streamedList)
    {
        return new(
            UserId.CreateUnique(),
            firstName,
            lastName,
            email,
            password,
            DateTime.UtcNow,
            DateTime.UtcNow,
            streamedList.Id
            );
    }

    public void VerifyEmail()
    {
        EmailVerified = true;
    }


    public enum UserClaimType
    {
        Editor
    }

    public void AddNewMovieList(MovieListId listId)
    {
        _movieListIds.Add(listId);
    }

    public void RemoveMovieList(MovieList movieList)
    {
        if(_movieListIds.Contains(movieList.Id))
        {
            _movieListIds.Remove(movieList.Id);
        }
    }

#pragma warning disable CS8618
    private User() { }
#pragma warning restore CS8618
}
