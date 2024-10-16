using MediatR;

namespace CSharpClicker.Web.UseCases.Login;

public record LoginCommand(string UserName, string Password) : IRequest<Unit>;
