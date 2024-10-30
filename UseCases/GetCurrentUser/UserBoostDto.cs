namespace CSharpClicker.Web.UseCases.GetCurrentUser;

public class UserBoostDto
{
    public int BoostId { get; init; }

    public long CurrentPrice { get; init; }

    public int Quantity { get; init; }
}
