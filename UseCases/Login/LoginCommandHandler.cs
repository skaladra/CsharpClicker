using CSharpClicker.Web.Domain;
using CSharpClicker.Web.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CSharpClicker.Web.UseCases.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Unit>
{
    private SignInManager<ApplicationUser> signInManager;
    private UserManager<ApplicationUser> userManager;

    public LoginCommandHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    public async Task<Unit> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ValidationException("Password is not correct.");
        }

        var user = await userManager.FindByNameAsync(request.UserName);

        if (user == null)
        {
            throw new NotFoundException("User was not found.");
        }

        await signInManager.PasswordSignInAsync(user, request.Password, isPersistent: true, lockoutOnFailure: false);

        return Unit.Value;
    }
}
