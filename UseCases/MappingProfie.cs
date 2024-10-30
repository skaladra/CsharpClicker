using AutoMapper;
using CSharpClicker.Web.Domain;
using CSharpClicker.Web.UseCases.GetBoosts;
using CSharpClicker.Web.UseCases.GetCurrentUser;

namespace CSharpClicker.Web.UseCases;

public class MappingProfie : Profile
{
    public MappingProfie()
    {
        CreateMap<Boost, BoostDto>();
        CreateMap<UserBoost, UserBoostDto>();
        CreateMap<ApplicationUser, UserDto>();
    }
}
