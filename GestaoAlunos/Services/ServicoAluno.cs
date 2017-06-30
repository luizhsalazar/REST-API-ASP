using GestaoAlunos.Interfaces;
using GestaoAlunos.Models;
using GestaoAlunos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoAlunos.Services
{
    public class ServicoAluno : IServicoAluno
    {
        protected IRepository<Aluno> AlunoRepository;
        protected IRepository<Curso> CursoRepository;
        protected IRepository<AlunoCurso> AlunoCursoRepository;

        public ServicoAluno(IRepository<Aluno> alunoRepository, IRepository<Curso> cursoRepository, IRepository<AlunoCurso> alunoCursoRepository)
        {
            AlunoRepository = alunoRepository;
            CursoRepository = cursoRepository;
            AlunoCursoRepository = alunoCursoRepository;
        }

        public List<Aluno> ListarAlunos()
        {
            return AlunoRepository.GetAll();
        }

        public Aluno Consultar(int id)
        {
            var consulta = AlunoRepository.BuscarComInclude(a => a.Id == id, "AlunoCursos.Curso.CursoDisciplinas.Disciplina").FirstOrDefault();
            return consulta;
        }

        public Aluno ExcluirAluno(Aluno aluno)
        {
            if (aluno.Id == 0)
            {
                throw new InvalidOperationException("Aluno não existe!");
            }

            var alunoBD = AlunoRepository.BuscarComInclude(a => a.Id == aluno.Id, a => a.AlunoCursos).FirstOrDefault();

            if (alunoBD.AlunoCursos.Count > 0)
            {
                throw new InvalidOperationException("Aluno está matriculado em algum curso. Não pode ser excluído.");
            }

            return AlunoRepository.Excluir(aluno);
        }

        public Aluno ExcluirAlunoCurso(Aluno aluno, Curso curso)
        {
            if (aluno.Id == 0)
            {
                throw new InvalidOperationException("Aluno informado não possui ID!");
            }

            if (curso.Id == 0)
            {
                throw new InvalidOperationException("Curso informado não possui ID!");
            }

            var cursoBD = CursoRepository.Consultar(curso.Id);

            if (cursoBD.Id == 0)
            {
                throw new InvalidOperationException("Curso não existe!");
            }

            var alunoCurso = AlunoCursoRepository.BuscarComInclude(ac => ac.Aluno.Id == aluno.Id, ac => ac.Aluno).ToList()
                                                    .Where(ac => ac.Curso.Id == cursoBD.Id).FirstOrDefault();
            
            if (! (alunoCurso is null))
            {
                AlunoCursoRepository.Excluir(alunoCurso);
            }

            return aluno;
        }

        public Aluno MatricularAluno(Aluno aluno)
        {
            AlunoRepository.Incluir(aluno);
            AlunoRepository.Salvar();

            return aluno;
        }

        public Aluno MatricularAlunoCurso(Aluno aluno, Curso curso)
        {
            if (aluno.Id == 0)
            {
                throw new InvalidOperationException("Aluno informado não possui ID!");
            }

            if (curso.Id == 0)
            {
                throw new InvalidOperationException("Curso informado não possui ID!");
            }

            var cursoBD = CursoRepository.Consultar(curso.Id);

            if (cursoBD.Id == 0)
            {
                throw new InvalidOperationException("Curso não existe!");
            }

            var alunoBD = AlunoRepository.BuscarComInclude(a => a.Id == aluno.Id, a => a.AlunoCursos).FirstOrDefault();

            if (!(alunoBD is null))
            {
                var alunoCurso = new AlunoCurso
                {
                    Aluno = alunoBD,
                    Curso = cursoBD
                };

                AlunoCursoRepository.Incluir(alunoCurso);
                AlunoCursoRepository.Salvar();
            }

            return aluno;
        }


        public Aluno ExcluirAlunoDisciplina(Aluno aluno, Curso curso)
        {
            throw new NotImplementedException();
        }

        public void Salvar()
        {
            AlunoRepository.Salvar();
        }
    }
}
