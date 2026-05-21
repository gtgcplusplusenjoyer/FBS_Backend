using FBS.Application.Interfaces;
using System.Security.Claims;

namespace FBS.Application.Services
{
    public class ChatAuthorizationService : IChatAuthorizationService
    {
        public bool CanDeleteMessage(string userRole)
        {
            return userRole == "Admin" || userRole == "Trainer";
        }

        public bool CanSendMessage(string userRole)
        {
            return userRole == "Admin" || userRole == "Trainer";
        }

        public bool CanClearChat(string userRole)
        {
            return userRole == "Admin";
        }
        public string GetErrorMessage( string action)
        {
            return $"У вас нет прав на {action}";
        }

        public string GetUserRole(ClaimsPrincipal user)
        {
            var role = user?.FindFirst(ClaimTypes.Role)?.Value ?? "Visitor";
            return role;
        }
    }
}
