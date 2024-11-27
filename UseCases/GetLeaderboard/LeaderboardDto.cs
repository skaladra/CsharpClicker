namespace CSharpClicker.Web.UseCases.GetLeaderboard;

public class LeaderboardDto
{
    public IReadOnlyCollection<LeaderboardUserDto> Users { get; init; }
}
