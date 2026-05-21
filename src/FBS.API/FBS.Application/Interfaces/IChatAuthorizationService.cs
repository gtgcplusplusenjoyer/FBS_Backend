using FBS.Core.Entities.User;
using System.Security.Claims;

namespace FBS.Application.Interfaces
{
    public interface IChatAuthorizationService
    {
        bool CanSendMessage(string userRole);
        bool CanDeleteMessage(string userRole);
        bool CanClearChat(string userRole);
        string GetErrorMessage(string userRole);
        string GetUserRole(ClaimsPrincipal user);
    }
}
