namespace CSharpClicker.Web.Domain;

public class UserBoost
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; }

    public int BoostId { get; set; }

    public Boost Boost { get; set; }

    public long CurrentPrice { get; set; }

    public int Quantity { get; set; }
}
