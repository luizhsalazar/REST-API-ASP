using GestaoAlunos.Models;
using GestaoAlunos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoAlunos.Services
{
    public class ServicoCurso
    {
        protected Repository<Curso> CursoRepository;

        public ServicoCurso(Repository<Curso> cursoRepository)
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

        public void Salvar()
        {
            CursoRepository.Salvar();
        }
    }
}
