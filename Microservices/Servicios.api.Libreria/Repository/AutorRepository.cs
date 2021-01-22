using MongoDB.Driver;
using Servicios.api.Libreria.Core.ContextMondoDB;
using Servicios.api.Libreria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Repository
{
    public class AutorRepository : IAutorRepository
    {
        private readonly IAutorContext context;

        public AutorRepository(IAutorContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Autor>> GetAutores()
        {
            return await context.Autores.Find(p => true).ToListAsync();
        }
    }
}
