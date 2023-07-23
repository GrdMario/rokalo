namespace Rokalo.Presentation.Api.Controllers.Accounts
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Net.Mime;

    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ApiControllerBase(
            IMediator mediator)
        {
            this.Mediator = mediator;
        }
        protected IMediator Mediator { get; }
    }
}
