using Application.Common.Interfaces.Authentication;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication;

public class GenerateToken : IGenerateToken
{
    public string GetToken(string userId)
    {
        //TODO - Secure the key
        userId = userId + "MySecretTokenKey";
        byte[] data = Encoding.UTF8.GetBytes(userId);
        byte[] hashBytes;

        using (MD5 md5 = MD5.Create())
        {
            hashBytes = md5.ComputeHash(data);
        };

        string hashString = BitConverter.ToString(hashBytes).Replace("-", "");
        return hashString;
    }
}
