using MediatR;

namespace CSharpClicker.Web.UseCases.GetUserSettings;

public record GetCurrentUserSettingsQuery : IRequest<UserSettingsDto>;
