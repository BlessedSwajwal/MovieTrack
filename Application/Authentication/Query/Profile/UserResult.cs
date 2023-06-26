using Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Query.Profile;

public record UserResult
(
    string FirstName,
    string LastName,
    string Email
);
