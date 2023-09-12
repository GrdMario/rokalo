namespace Rokalo.Presentation.Api.Controllers.Accounts
{
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Rokalo.Application.User.Commands;
    using System;
    using System.Threading.Tasks;

    public class AccountsController : ApiControllerBase
    {
        public AccountsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Post([FromBody] RegisterUserCommand request)
        {
            await this.Mediator.Send(request);

            return this.NoContent();
        }

        [HttpPost("email-confirmation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommand request)
        {
            await this.Mediator.Send(request);

            return this.NoContent();
        }

        [HttpPost("resend-email-confirmation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> ResendEmailConfirmation([FromBody] ResendConfirmationEmailCommand request)
        {
            await this.Mediator.Send(request);

            return this.NoContent();
        }

    }
}
