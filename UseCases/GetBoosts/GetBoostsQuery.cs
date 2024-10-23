using MediatR;

namespace CSharpClicker.Web.UseCases.GetBoosts;

public record GetBoostsQuery : IRequest<IReadOnlyCollection<BoostDto>>;
