using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private ISender? _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
        //private ICurrentUser _currentUser;

        //protected ICurrentUser CurrentUser => _currentUser ?? (_currentUser = HttpContext.RequestServices.GetService<ICurrentUser>());
    }
}
