using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Profile;

public record ProfileResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    float WatchTime
);