namespace CSharpClicker.Web.UseCases.GetLeaderboard;

public class LeaderboardDto
{
    public IReadOnlyCollection<LeaderboardUserDto> Users { get; init; }

    public PageInfoDto PageInfo { get; init; }
}
