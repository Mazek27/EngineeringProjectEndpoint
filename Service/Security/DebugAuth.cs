using System.Security.Claims;

namespace Engineering_Project.Service.Security
{
    public class DebugAuth
    {
        private static string userName = "Mazek27";
        
        public static string getUserName(ClaimsPrincipal User)
        {
            #if DEBUG
                return userName;
            #else
                return User.FindFirstValue(ClaimTypes.NameIdentifier);
            #endif
        }
    }
}