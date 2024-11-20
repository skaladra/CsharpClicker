using CSharpClicker.Web.UseCases.AddScore;
using CSharpClicker.Web.UseCases.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace CSharpClicker.Web.Controllers;

[Route("score")]
public class ScoreController : ControllerBase
{
    private readonly IMediator mediator;

    public ScoreController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("add")]
    public async Task<ScoreDto> AddScore(AddScoreCommand command)
        => await mediator.Send(command);
}
