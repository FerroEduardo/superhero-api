using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SuperHeroAPI.Util
{
    public class RequestUtil
    {
        public static int GetUserId(ControllerBase controller)
        {
            return int.Parse(controller.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}
