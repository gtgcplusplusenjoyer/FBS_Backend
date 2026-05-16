using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FBS.API.Extensions;

namespace FBS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApiController : ControllerBase
    {
        private Guid? _userId;

        protected Guid UserId
        {
            get
            {
                if (_userId == null)
                {

                    _userId = User.GetUserId();

                    if(_userId == null)
                    {
                        throw new UnauthorizedAccessException("User id not found in token");
                    }

                }
                return _userId.Value;
            }
        }

    }
}
