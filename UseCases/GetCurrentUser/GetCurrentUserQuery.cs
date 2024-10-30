using MediatR;

namespace CSharpClicker.Web.UseCases.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<UserDto>;
