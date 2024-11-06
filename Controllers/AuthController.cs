using CSharpClicker.Web.UseCases.Login;
using CSharpClicker.Web.UseCases.Logout;
using CSharpClicker.Web.UseCases.Register;
using CSharpClicker.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CSharpClicker.Web.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    private readonly IMediator mediator;

    public AuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        try
        {
            await mediator.Send(command);
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            var viewModel = new AuthViewModel
            {
                UserName = command.UserName,
                Password = command.Password,
            };

            return View(viewModel);
        }

        return RedirectToAction(nameof(Login));
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View(new AuthViewModel());
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        try
        {
            await mediator.Send(command);
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            var viewModel = new AuthViewModel
            {
                UserName = command.UserName,
                Password = command.Password,
            };

            return View(viewModel);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View(new AuthViewModel());
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogoutCommand command)
    {
        await mediator.Send(command);

        return RedirectToAction("Login");
    }
}
