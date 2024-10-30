﻿using CSharpClicker.Web.UseCases.AddPoints;
using CSharpClicker.Web.UseCases.GetBoosts;
using CSharpClicker.Web.UseCases.GetCurrentUser;
using CSharpClicker.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CSharpClicker.Web.Controllers;

public class HomeController : Controller
{
    private readonly IMediator mediator;

    public HomeController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var boosts = await mediator.Send(new GetBoostsQuery());
        var user = await mediator.Send(new GetCurrentUserQuery());

        var viewModel = new IndexViewModel()
        {
            Boosts = boosts,
            User = user,
        };

        return View(viewModel);
    }

    [HttpPost("click")]
    public async Task<IActionResult> Click()
    {
        await mediator.Send(new AddPointsCommand(Times: 1));

        return RedirectToAction(nameof(Index));
    }
}