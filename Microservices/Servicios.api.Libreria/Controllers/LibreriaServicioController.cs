using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.api.Libreria.Core.Entities;
using Servicios.api.Libreria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibreriaServicioController : ControllerBase
    {
        private readonly IMongoRepository<AutorEntity> autorGenericoRepository;
        private readonly IMongoRepository<EmpleadoEntity> empleadoGenericoRepository;

        public LibreriaServicioController(IMongoRepository<AutorEntity> autorGenericoRepository, IMongoRepository<EmpleadoEntity> empleadoGenericoRepository)
        {
            this.autorGenericoRepository = autorGenericoRepository;
            this.empleadoGenericoRepository = empleadoGenericoRepository;
        }

        //private readonly IAutorRepository autorRepository;

        //public LibreriaServicioController(IAutorRepository autorRepository)
        //{
        //    this.autorRepository = autorRepository;
        //}


        [HttpGet]
        public async Task<ActionResult<ICollection<AutorEntity>>> GetAutores()
        {
            //var autores = await this.autorRepository.GetAutores();
            var autores = await this.autorGenericoRepository.GetAll();
            return Ok(autores);
        }

        [HttpGet("empleados")]
        public async Task<ActionResult<ICollection<EmpleadoEntity>>> GetEmpleados()
        {
            var empleados = await this.empleadoGenericoRepository.GetAll();
            return Ok(empleados);
        }

    }
}
