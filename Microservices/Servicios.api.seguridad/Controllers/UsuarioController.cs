using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.api.seguridad.Core.Application;
using Servicios.api.seguridad.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.seguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsuarioController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioDto>> Registrar(Register.UsuarioRegisterCommand param)
        {
            return await mediator.Send(param);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login(Login.UsuarioLoginCommand param)
        {
            return await mediator.Send(param);
        }

        [HttpGet]
        public async Task<ActionResult<UsuarioDto>> UsuarioActual()
        {
            return await mediator.Send(new UsuarioActual.UsuarioActualCommand());
        }

    }
}
