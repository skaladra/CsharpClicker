using MediatR;

namespace CSharpClicker.Web.UseCases.AddPoints;

public record AddPointsCommand(int Clicks, int Seconds) : IRequest<Unit>;
