using CSharpClicker.Web.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CSharpClicker.Web.UseCases.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly UserManager<ApplicationUser> userManager;

    public RegisterCommandHandler(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (userManager.Users.Any(u => u.UserName == request.UserName))
        {
            throw new ValidationException("Such user already exists.");
        }

        var user = new ApplicationUser
        {
            UserName = request.UserName,
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errorString = string.Join(Environment.NewLine, result.Errors);
            throw new ValidationException(errorString);
        }

        return Unit.Value;
    }
}
