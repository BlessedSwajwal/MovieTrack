﻿using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string Email);
    void Add(User user);

    void SaveChanges();
}
