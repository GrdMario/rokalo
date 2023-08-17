namespace Rokalo.Presentation.Api.Controllers.Accounts
{
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Rokalo.Application.User.Commands;
    using System.Threading.Tasks;

    public class AccountsController : ApiControllerBase
    {
        public AccountsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Post([FromBody] RegisterUserCommand request)
        {
            await this.Mediator.Send(request);

            return this.NoContent();
        }

    }
}
