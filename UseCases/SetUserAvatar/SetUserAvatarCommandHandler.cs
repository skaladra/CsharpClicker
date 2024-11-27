using CSharpClicker.Web.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.UseCases.SetUserAvatar;

public class SetUserAvatarCommandHandler : IRequestHandler<SetUserAvatarCommand, Unit>
{
    private readonly ICurrentUserAccessor currentUserAccessor;
    private readonly IAppDbContext appDbContext;

    public SetUserAvatarCommandHandler(ICurrentUserAccessor currentUserAccessor, IAppDbContext appDbContext)
    {
        this.currentUserAccessor = currentUserAccessor;
        this.appDbContext = appDbContext;
    }

    public async Task<Unit> Handle(SetUserAvatarCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserAccessor.GetCurrentUserId();

        var user = await appDbContext.ApplicationUsers.FirstAsync(user => user.Id == userId, cancellationToken);

        using var memoryMemory = new MemoryStream();
        await request.Avatar.CopyToAsync(memoryMemory, cancellationToken);

        user.Avatar = memoryMemory.ToArray();

        await appDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
