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
    public class Register
    {
        public class UsuarioRegisterCommand: IRequest<UsuarioDto>
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UsuarioRegisterValidation : AbstractValidator<UsuarioRegisterCommand>
        {
            public UsuarioRegisterValidation()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();

            }
        }

        public class Handler : IRequestHandler<UsuarioRegisterCommand, UsuarioDto>
        {
            private readonly IMapper mapper;
            private readonly UserManager<Usuario> usuarioManager;
            private readonly SeguridadContexto context;
            private readonly IJwtGenerator jwt;

            public Handler(IMapper mapper, UserManager<Usuario> usuarioManager, SeguridadContexto context, IJwtGenerator jwt)
            {
                this.mapper = mapper;
                this.usuarioManager = usuarioManager;
                this.context = context;
                this.jwt = jwt;
            }

            public async Task<UsuarioDto> Handle(UsuarioRegisterCommand request, CancellationToken cancellationToken)
            {
                var existe = await context.Users.Where(x => x.Email == request.Email || x.UserName == request.Username).AnyAsync();
                if (existe) throw new Exception("Usuario o email existente");
                

                var usuario = new Usuario
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    Email = request.Email,
                    UserName = request.Username
                };

                var result = await usuarioManager.CreateAsync(usuario, request.Password);
                if (!result.Succeeded) throw new Exception("Error al crear el usuario");

                usuario = await context.Users.Where(x => x.Email == request.Email).SingleAsync();

                var usuarioDto = mapper.Map<UsuarioDto>(usuario);
                usuarioDto.Token = jwt.CreateToken(usuario);

                return usuarioDto;
            }
        }
    }
}
