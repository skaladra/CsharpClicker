namespace CSharpClicker.Web.UseCases.GetCurrentUser;

public class UserDto
{
    public string UserName { get; init; }

    public long CurrentScore { get; init; }

    public long RecordScore { get; init; }

    public IReadOnlyCollection<UserBoostDto> UserBoosts { get; init; }

    public byte[] Avatar { get; init; }

    public long ProfitPerClick { get; set; }

    public long ProfitPerSecond { get; set; }
}
