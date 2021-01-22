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
    public class LibroController : ControllerBase
    {
        private readonly IMongoRepository<LibroEntity> libroRepository;

        public LibroController(IMongoRepository<LibroEntity> libroRepository)
        {
            this.libroRepository = libroRepository;
        }

        [HttpPost]
        public async Task Post(LibroEntity libro)
        {
            await libroRepository.InsertDocument(libro);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroEntity>>> GetAll()
        {
            return Ok(await libroRepository.GetAll());
        }

        [HttpPost("pagination")]
        public async Task<ActionResult<PaginationEntity<LibroEntity>>> GetPagination(PaginationEntity<LibroEntity> pagination)
        {
            return Ok(await libroRepository.PaginationByFilter(pagination));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroEntity>> GetById(string id)
        {
            return Ok(await libroRepository.GetById(id));
        }
    }
}
