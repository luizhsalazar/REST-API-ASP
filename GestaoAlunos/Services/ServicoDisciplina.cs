using GestaoAlunos.Models;
using GestaoAlunos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoAlunos.Services
{
    public class ServicoDisciplina
    {
        protected Repository<CursoDisciplina> CursoDisciplinaRepository;
        protected Repository<Disciplina> DisciplinaRepository;
        protected Repository<Curso> CursoRepository;

        public ServicoDisciplina(Repository<CursoDisciplina> cursoDisciplinaRepository,
                                      Repository<Disciplina> disciplinaRepository, Repository<Curso> cursoRepository)
        {
            CursoDisciplinaRepository = cursoDisciplinaRepository;
            DisciplinaRepository = disciplinaRepository;
            CursoRepository = cursoRepository;
        }

        public Disciplina IncluirDisciplina(Disciplina disciplina)
        {
            if (disciplina.Nome != "" && DisciplinaRepository.BuscarComInclude(d => d.Nome == disciplina.Nome).FirstOrDefault().Id > 0)
            {
                throw new InvalidOperationException("Disciplina com mesmo nome já existe no banco!");
            }

            DisciplinaRepository.Incluir(disciplina);
            DisciplinaRepository.Salvar();

            return disciplina;
        }

        public Disciplina IncluirDisciplinaCurso(Disciplina disciplina, Curso curso)
        {
            if (disciplina.Id == 0)
            {
                throw new InvalidOperationException("Disciplina informado não possui ID!");
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

            var disciplinaBD = DisciplinaRepository.Consultar(disciplina.Id);

            if (disciplinaBD.Id == 0)
            {
                throw new InvalidOperationException("Disciplina não existe!");
            }

            var disciplinaCurso = new CursoDisciplina
            {
                Curso = cursoBD,
                Disciplina = disciplinaBD
            };

            CursoDisciplinaRepository.Incluir(disciplinaCurso);
            CursoDisciplinaRepository.Salvar();

            return disciplina;
        }

        public Disciplina EditarDisciplina(Disciplina disciplina)
        {
            if (disciplina.Id == 0)
            {
                throw new InvalidOperationException("Disciplina informado não possui ID!");
            }

            var disciplinaBD = DisciplinaRepository.BuscarComInclude(d => d.Id == disciplina.Id, d => d.DisciplinaCursos).FirstOrDefault();

            if (disciplinaBD.DisciplinaCursos.Count > 0)
            {
                throw new InvalidOperationException("Disciplina possui curso atrelado. Não pode ser editado.");
            }

            DisciplinaRepository.Alterar(disciplina);
            DisciplinaRepository.Salvar();

            return disciplina;
        }

        public Disciplina ExcluirDisciplina(Disciplina disciplina)
        {
            if (disciplina.Id == 0)
            {
                throw new InvalidOperationException("Disciplina informado não possui ID!");
            }

            var disciplinaBD = DisciplinaRepository.BuscarComInclude(d => d.Id == disciplina.Id, d => d.DisciplinaCursos).FirstOrDefault();

            if (disciplinaBD.DisciplinaCursos.Count > 0)
            {
                throw new InvalidOperationException("Disciplina possui curso atrelado. Não pode ser excluído.");
            }

            DisciplinaRepository.Excluir(disciplina);
            DisciplinaRepository.Salvar();

            return disciplina;
        }

        public Disciplina ExcluirDisciplinaCurso(Disciplina disciplina, Curso curso)
        {
            if (disciplina.Id == 0)
            {
                throw new InvalidOperationException("Disciplina informado não possui ID!");
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

            var disciplinaBD = DisciplinaRepository.BuscarComInclude(d => d.Id == disciplina.Id, "DisciplinaCursos.Curso.CursoAlunos").FirstOrDefault();

            if (disciplinaBD is null)
            {
                throw new InvalidOperationException("Disciplina não existe!");
            }

            var consultado = disciplinaBD.DisciplinaCursos.FirstOrDefault(x => x.CursoId == cursoBD.Id);

            if (!(consultado is null))
            {
                if (consultado.Curso.CursoAlunos.Count > 0)
                {
                    throw new InvalidOperationException("Disciplina possui vínculo com curso com aluno matriculado.");
                }

                CursoDisciplinaRepository.Excluir(consultado);
                CursoDisciplinaRepository.Salvar();
            }
            else
            {
                throw new InvalidOperationException("Disciplina não possui vínculo com curso.");
            }

            return disciplinaBD;
        }
    }
}
