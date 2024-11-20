using CSharpClicker.Web.UseCases.Common;
using MediatR;

namespace CSharpClicker.Web.UseCases.BuyBoost;

public record BuyBoostCommand(int BoostId) : IRequest<ScoreBoostDto>;
