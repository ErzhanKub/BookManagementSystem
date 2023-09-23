﻿using Application.Users.Commands;
using Application.Users.Commands.Delete;
using Application.Users.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAllUsersRequest();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("GetByName")]
        public async Task<IActionResult> GetByName(GetUserByNameCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(user);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdateUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("DeleteByName")]
        public async Task<IActionResult> Delete(DeleteUserByNameCommand command)
        {
            var response = await _mediator.Send(command);
            if (response == true)
                return Ok(response);
            return BadRequest();
        }

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> Delete(DeleteUserByIdCommand command)
        {
            var response = await _mediator.Send(command);
            if (response == true)
                return Ok(response);
            return BadRequest();
        }
    }
}
