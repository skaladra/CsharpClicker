using CSharpClicker.Web.UseCases.Common;
using MediatR;

namespace CSharpClicker.Web.UseCases.AddScore;

public record AddScoreCommand(int Clicks, int Seconds) : IRequest<ScoreDto>;
