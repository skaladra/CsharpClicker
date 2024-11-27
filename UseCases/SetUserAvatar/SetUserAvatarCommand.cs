using MediatR;

namespace CSharpClicker.Web.UseCases.SetUserAvatar;

public record SetUserAvatarCommand(IFormFile Avatar) : IRequest<Unit>;
