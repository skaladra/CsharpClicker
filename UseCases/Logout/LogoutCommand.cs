using MediatR;

namespace CSharpClicker.Web.UseCases.Logout;

public record LogoutCommand : IRequest<Unit>;
