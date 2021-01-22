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
    public class LibreriaAutorController : ControllerBase
    {
        private readonly IMongoRepository<AutorEntity> autorGenericoRepository;

        public LibreriaAutorController(IMongoRepository<AutorEntity> autorGenericoRepository)
        {
            this.autorGenericoRepository = autorGenericoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorEntity>>> GetAll()
        {
            return Ok(await autorGenericoRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorEntity>> Get(string id)
        {
            return Ok(await autorGenericoRepository.GetById(id));
        }

        [HttpPost]
        public async Task Post(AutorEntity autor)
        {
            await autorGenericoRepository.InsertDocument(autor);
        }

        [HttpPut("{id}")]
        public async Task Put(string id, AutorEntity autor)
        {
            autor.Id = id;
            await autorGenericoRepository.UpdateDocument(autor);
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await autorGenericoRepository.DeleteById(id);

        }

        [HttpPost("pagination")]
        public async Task<ActionResult<PaginationEntity<AutorEntity>>> PostPagination(PaginationEntity<AutorEntity> pagination)
        {
            var resultados = await autorGenericoRepository.PaginationByFilter(pagination);
            //var resultados = await autorGenericoRepository.PaginationBy(
            //    filter => filter.Nombre.ToLower() == pagination.Filter.ToLower(),
            //    pagination
            //    );
            return Ok(resultados);
        }
    }
}