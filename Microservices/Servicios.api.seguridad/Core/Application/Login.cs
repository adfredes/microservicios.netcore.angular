using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Servicios.api.seguridad.Core.Dto;
using Servicios.api.seguridad.Core.Entity;
using Servicios.api.seguridad.Core.JwtLogic;
using Servicios.api.seguridad.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servicios.api.seguridad.Core.Application
{
    public class Login
    {
        public class UsuarioLoginCommand : IRequest<UsuarioDto>
        {            
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UsuarioLoginValidation : AbstractValidator<UsuarioLoginCommand>
        {
            public UsuarioLoginValidation()
            {                
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();

            }
        }

        public class Handler : IRequestHandler<UsuarioLoginCommand, UsuarioDto>
        {
            private readonly IMapper mapper;
            private readonly UserManager<Usuario> usuarioManager;
            private readonly SeguridadContexto context;
            private readonly IJwtGenerator jwt;
            private readonly SignInManager<Usuario> signInManager;

            public Handler(IMapper mapper, UserManager<Usuario> usuarioManager, SeguridadContexto context, IJwtGenerator jwt, SignInManager<Usuario> signInManager)
            {
                this.mapper = mapper;
                this.usuarioManager = usuarioManager;
                this.context = context;
                this.jwt = jwt;
                this.signInManager = signInManager;
            }

            public async Task<UsuarioDto> Handle(UsuarioLoginCommand request, CancellationToken cancellationToken)
            {
                var usuario = await usuarioManager.FindByEmailAsync(request.Email);
                if (usuario == null) throw new Exception("Email o contraseña incorrecto");

                var result = await signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);
                //var result = await usuarioManager.CreateAsync(usuario, request.Password);
                if (!result.Succeeded) throw new Exception("Email o contraseña incorrecto");                

                var usuarioDto = mapper.Map<UsuarioDto>(usuario);
                usuarioDto.Token = jwt.CreateToken(usuario);

                return usuarioDto;
            }
        }
    }
}
