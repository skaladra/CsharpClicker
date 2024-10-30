using CSharpClicker.Web.UseCases.GetBoosts;
using CSharpClicker.Web.UseCases.GetCurrentUser;

namespace CSharpClicker.Web.ViewModels;

public class IndexViewModel
{
    public UserDto User { get; init; }

    public IReadOnlyCollection<BoostDto> Boosts { get; init; }
}
