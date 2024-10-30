namespace CSharpClicker.Web.Infrastructure.Abstractions;

public interface ICurrentUserAccessor
{
    Guid GetCurrentUserId();
}
