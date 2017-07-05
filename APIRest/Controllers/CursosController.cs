using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestaoAlunos.Interfaces;
using GestaoAlunos.Models;

namespace APIRest.Controllers
{
    [Route("api/[controller]")]
    public class CursosController : Controller
    {
        protected IServicoCurso ServicoCurso;

        public CursosController(IServicoCurso servicoCurso)
        {
            ServicoCurso = servicoCurso;
        }

        [HttpGet]
        public List<Curso> Get()
        {
            return ServicoCurso.ListarCursosComRelations();
        }

        // GET: api/Default/5
        [HttpGet("{id}", Name = "Get")]
        public Curso Get(int id)
        {
            var curso = ServicoCurso.Consultar(id);
            return curso;
        }

        // POST: api/Default
        [HttpPost]
        public Curso Post([FromBody]Curso curso)
        {
            var cursoIncluido = ServicoCurso.Incluir(curso);
            return cursoIncluido;
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
