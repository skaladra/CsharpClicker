using CSharpClicker.Web.UseCases.GetBoosts;

namespace CSharpClicker.Web.ViewModels;

public class IndexViewModel
{
    public IReadOnlyCollection<BoostDto> Boosts { get; init; }
}
