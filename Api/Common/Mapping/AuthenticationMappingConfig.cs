using Application.Authentication.Common;
using Contract.Authentication;
using Mapster;

namespace Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
     .Map(dest => dest.Id, src => Guid.Parse($"{src.User.Id.Value}"))
     .Map(dest => dest.FirstName, src => src.User.FirstName)
     .Map(dest => dest.LastName, src => src.User.LastName)
     .Map(dest => dest.Email, src => src.User.Email)
     .Map(dest => dest.Token, src => src.Token)
     .IgnoreNullValues(true);

    }
}
