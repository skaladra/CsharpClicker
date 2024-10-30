using MediatR;

namespace CSharpClicker.Web.UseCases.AddPoints;

public record AddPointsCommand(int Times, bool IsAuto = false) : IRequest<Unit>;
