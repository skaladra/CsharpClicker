using CSharpClicker.Web.DomainServices;
using CSharpClicker.Web.Infrastructure.Abstractions;
using CSharpClicker.Web.UseCases.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.UseCases.AddPoints;

public class AddPointsCommandHandler : IRequestHandler<AddPointsCommand, ScoreDto>
{
    private readonly ICurrentUserAccessor currentUserAccessor;
    private readonly IAppDbContext appDbContext;

    public AddPointsCommandHandler(ICurrentUserAccessor currentUserAccessor, IAppDbContext appDbContext)
    {
        this.currentUserAccessor = currentUserAccessor;
        this.appDbContext = appDbContext;
    }

    public async Task<ScoreDto> Handle(AddPointsCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserAccessor.GetCurrentUserId();
        var user = await appDbContext.ApplicationUsers
            .Include(user => user.UserBoosts)
            .ThenInclude(ub => ub.Boost)
            .FirstAsync(user => user.Id == userId);

        var profitPerSecond = user.UserBoosts.GetProfit(shouldCalculateAutoBoosts: true);
        var profitPerClick = user.UserBoosts.GetProfit();

        var autoPoints = profitPerSecond * request.Seconds;
        var clickedPoints = profitPerClick * request.Clicks;

        user.CurrentScore += autoPoints + clickedPoints;
        user.RecordScore += autoPoints + clickedPoints;

        await appDbContext.SaveChangesAsync();

        return new ScoreDto
        {
            CurrentScore = user.CurrentScore,
            RecordScore = user.RecordScore,
            ProfitPerClick = profitPerClick,
            ProfitPerSecond = profitPerSecond,
        };
    }
}
