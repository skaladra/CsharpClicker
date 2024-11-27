using MediatR;

namespace CSharpClicker.Web.UseCases.GetLeaderboard;

public class GetLeaderboardQuery : IRequest<LeaderboardDto>;
