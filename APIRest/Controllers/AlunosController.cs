using GestaoAlunos.Context;
using GestaoAlunos.Interfaces;
using GestaoAlunos.Models;
using GestaoAlunos.Repository;
using GestaoAlunos.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Controllers
{
    [Route("api/[controller]")]
    public class AlunosController : Controller
    {
        //public static DataContext Context = new DataContext();
        //protected static Repository<Curso> RepositorioCurso = new Repository<Curso>(Context);
        //protected static Repository<Aluno> RepositorioAluno = new Repository<Aluno>(Context);
        //protected static Repository<AlunoCurso> RepositorioAlunoCurso = new Repository<AlunoCurso>(Context);

        protected IServicoAluno ServicoAluno; // = new ServicoAluno(RepositorioAluno, RepositorioCurso, RepositorioAlunoCurso);

        public AlunosController(IServicoAluno servicoAluno)
        {
            ServicoAluno = servicoAluno;
        }

        // GET api/alunos
        [HttpGet]
        public List<Aluno> Get()
        {            
            return ServicoAluno.ListarAlunos();
        }

        [HttpGet("{id}")]
        public Aluno Get(int id)
        {
            var aluno = ServicoAluno.Consultar(id);
            return aluno;
        }

        [HttpPost]
        public Aluno Post([FromBody]Aluno novoAluno)
        {
            var aluno = ServicoAluno.MatricularAluno(novoAluno);
            return aluno;
        }
    }
}
