using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]/{action}")]
public abstract class Controller : ControllerBase
{
    // Base
}
