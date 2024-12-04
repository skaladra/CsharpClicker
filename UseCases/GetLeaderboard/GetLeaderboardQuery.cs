using MediatR;

namespace CSharpClicker.Web.UseCases.GetLeaderboard;

public record GetLeaderboardQuery(int Page = 1) : IRequest<LeaderboardDto>;
