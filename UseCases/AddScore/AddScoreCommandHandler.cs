using CSharpClicker.Web.DomainServices;
using CSharpClicker.Web.Infrastructure.Abstractions;
using CSharpClicker.Web.UseCases.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.UseCases.AddScore;

public class AddScoreCommandHandler : IRequestHandler<AddScoreCommand, ScoreDto>
{
    private readonly ICurrentUserAccessor currentUserAccessor;
    private readonly IAppDbContext appDbContext;

    public AddScoreCommandHandler(ICurrentUserAccessor currentUserAccessor, IAppDbContext appDbContext)
    {
        this.currentUserAccessor = currentUserAccessor;
        this.appDbContext = appDbContext;
    }

    public async Task<ScoreDto> Handle(AddScoreCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserAccessor.GetCurrentUserId();
        var user = await appDbContext.ApplicationUsers
            .Include(user => user.UserBoosts)
            .ThenInclude(ub => ub.Boost)
            .FirstAsync(user => user.Id == userId, cancellationToken);

        var profitPerClick = user.UserBoosts.GetProfit();
        var profitPerSecond = user.UserBoosts.GetProfit(shouldCalculateAutoBoosts: true);

        var pointsForClicks = profitPerClick * request.Clicks;
        var pointsForSeconds = profitPerSecond * request.Seconds;

        var totalPoints = pointsForSeconds + pointsForClicks;

        user.CurrentScore += totalPoints;
        user.RecordScore += totalPoints;

        await appDbContext.SaveChangesAsync(cancellationToken);

        return new ScoreDto
        {
            CurrentScore = user.CurrentScore,
            RecordScore = user.RecordScore,
            ProfitPerClick = profitPerClick,
            ProfitPerSecond = profitPerSecond,
        };
    }
}
