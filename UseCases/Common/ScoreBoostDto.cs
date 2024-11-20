namespace CSharpClicker.Web.UseCases.Common;

public record ScoreBoostDto
{
    public required ScoreDto Score { get; init; }

    public int Quantity { get; init; }

    public long Price { get; init; }
}
