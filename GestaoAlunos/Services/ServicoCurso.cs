using GestaoAlunos.Interfaces;
using GestaoAlunos.Models;
using GestaoAlunos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoAlunos.Services
{
    public class ServicoCurso : IServicoCurso
    {
        protected IRepository<Curso> CursoRepository;

        public ServicoCurso(IRepository<Curso> cursoRepository)
        {
            CursoRepository = cursoRepository;
        }

        public Curso Incluir(Curso curso)
        {
            if (curso.CursoDisciplinas.Count == 0)
            {
                throw new InvalidOperationException("Este curso não possui disciplinas atreladas a ele.");
            }

            CursoRepository.Incluir(curso);
            CursoRepository.Salvar();

            return curso;
        }

        public List<Curso> ListarCursos()
        {
            return CursoRepository.GetAll();
        }

        public List<Curso> ListarCursosComRelations()
        {
            return CursoRepository.GetAll("CursoDisciplinas.Disciplina", "CursoAlunos.Aluno");
        }

        public void Salvar()
        {
            CursoRepository.Salvar();
        }

        public Curso Consultar(int id)
        {
            var consulta = CursoRepository.BuscarComInclude(a => a.Id == id, "CursoDisciplinas.Disciplina", "CursoAlunos.Aluno").FirstOrDefault();
            return consulta;
        }

        public Curso ExcluirCurso(Curso Curso)
        {
            throw new NotImplementedException();
        }

        public Curso ExcluirCursoCurso(Curso Curso, Curso curso)
        {
            throw new NotImplementedException();
        }

        public Curso ExcluirCursoDisciplina(Curso Curso, Curso curso)
        {
            throw new NotImplementedException();
        }        

        public Curso MatricularCurso(Curso Curso)
        {
            throw new NotImplementedException();
        }

        public Curso MatricularCursoCurso(Curso Curso, Curso curso)
        {
            throw new NotImplementedException();
        }
    }
}
