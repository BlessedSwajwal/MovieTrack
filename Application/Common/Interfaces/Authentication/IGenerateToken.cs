namespace Application.Common.Interfaces.Authentication;

public interface IGenerateToken
{
    string GetToken(string userId);
}