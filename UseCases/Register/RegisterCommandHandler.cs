using CSharpClicker.Web.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

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
        var user = new ApplicationUser()
        {
            UserName = request.UserName,
        };

        await userManager.CreateAsync(user, request.Password);

        return Unit.Value;
    }
}
    