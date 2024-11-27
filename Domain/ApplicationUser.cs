using Microsoft.AspNetCore.Identity;

namespace CSharpClicker.Web.Domain;

public class ApplicationUser : IdentityUser<Guid>
{
    public long CurrentScore { get; set; }

    public long RecordScore { get; set; }

    public ICollection<UserBoost> UserBoosts { get; set; } = [];

    public byte[] Avatar { get; set; } = [];
}
