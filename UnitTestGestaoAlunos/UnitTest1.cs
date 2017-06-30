using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GestaoAlunos.Models;
using GestaoAlunos.Context;
using GestaoAlunos.Repository;
using GestaoAlunos.Services;
using System.Collections.Generic;

namespace UnitTestGestaoAlunos
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }

        public static DataContext Context = new DataContext();
        protected static Repository<Curso> RepositorioCurso = new Repository<Curso>(Context);
        protected static Repository<Disciplina> RepositorioDisciplina = new Repository<Disciplina>(Context);
        protected static Repository<Aluno> RepositorioAluno = new Repository<Aluno>(Context);
        protected static Repository<AlunoCurso> RepositorioAlunoCurso = new Repository<AlunoCurso>(Context);
        protected static Repository<CursoDisciplina> RepositorioCursoDisciplina = new Repository<CursoDisciplina>(Context);

        protected ServicoCurso ServicoCurso = new ServicoCurso(RepositorioCurso);
        protected ServicoAluno ServicoAluno = new ServicoAluno(RepositorioAluno, RepositorioCurso, RepositorioAlunoCurso);
        protected ServicoDisciplina ServicoDisciplina = new ServicoDisciplina(RepositorioCursoDisciplina, RepositorioDisciplina, RepositorioCurso);

        protected List<Aluno> Alunos = new List<Aluno>
        {
            new Aluno { Nome = "Luiz Henrique Salazar", DataCriacao = DateTime.Now },
            new Aluno { Nome = "Sérgio Martins", DataCriacao = DateTime.Now },
            new Aluno { Nome = "Felipe da Silva", DataCriacao = DateTime.Now },
            new Aluno { Nome = "Alexandre Pereira", DataCriacao = DateTime.Now },
            new Aluno { Nome = "Severino João", DataCriacao = DateTime.Now }
        };

        protected List<Curso> Cursos = new List<Curso>
        {
            new Curso { Nome = "Ciências da Computação", DataCriacao = DateTime.Now },
            new Curso { Nome = "Sistema de Informação", DataCriacao = DateTime.Now },
            new Curso { Nome = "Engenharia da Computação", DataCriacao = DateTime.Now },
            new Curso { Nome = "Engenharia Mecânica", DataCriacao = DateTime.Now },
            new Curso { Nome = "Arquitetura", DataCriacao = DateTime.Now }
        };

        protected List<Disciplina> Disciplinas = new List<Disciplina>
        {
            new Disciplina { Nome = "Cálculo I", DataCriacao = DateTime.Now },
            new Disciplina { Nome = "Cálculo II", DataCriacao = DateTime.Now },
            new Disciplina { Nome = "Cálculo III", DataCriacao = DateTime.Now },
            new Disciplina { Nome = "Introdução a Computação", DataCriacao = DateTime.Now },
            new Disciplina { Nome = "Engenharia de Software", DataCriacao = DateTime.Now }
        };

        //[TestMethod]
        //public void TestMethodPopularBanco()
        //{
        //    Alunos.ForEach(a => RepositorioAluno.Incluir(a));
        //    Cursos.ForEach(c => RepositorioCurso.Incluir(c));
        //    Disciplinas.ForEach(d => RepositorioDisciplina.Incluir(d));
        //}

        [TestMethod]
        public void TestMethodIncluirCursoSemDisciplina()
        {
            var curso = new Curso
            {
                Nome = "Ciências da Computação",
                DataCriacao = DateTime.Now
            };

            Assert.ThrowsException<InvalidOperationException>(() => ServicoCurso.Incluir(curso));
        }

        [TestMethod]
        public void TestMethodIncluirCursoComDisciplina()
        {
            var curso = new Curso
            {
                Nome = "Ciências da Computação",
                DataCriacao = DateTime.Now
            };

            var disciplina = new Disciplina
            {
                Nome = "Cálculo I",
                DataCriacao = DateTime.Now
            };

            var disciplinaCurso = new CursoDisciplina
            {
                Disciplina = disciplina,
                Curso = curso
            };

            curso.CursoDisciplinas.Add(disciplinaCurso);
            var cursoIncluido = ServicoCurso.Incluir(curso);
            ServicoCurso.Salvar();

            Assert.IsNotNull(cursoIncluido);
        }

        [TestMethod]
        public void TestMethodMatricularAluno()
        {
            var aluno = new Aluno
            {
                Nome = "Luiz Henrique Salazar",
                DataCriacao = DateTime.Now
            };

            var alunoMatriculado = ServicoAluno.MatricularAluno(aluno);
            ServicoAluno.Salvar();

            Assert.IsNotNull(alunoMatriculado);
        }

        [TestMethod]
        public void TestMethodMatricularAlunoCurso()
        {
            var curso = RepositorioCurso.Consultar(2);
            var aluno = RepositorioAluno.Consultar(1);

            var alunoMatriculadoCurso = ServicoAluno.MatricularAlunoCurso(aluno, curso);

            Assert.IsNotNull(alunoMatriculadoCurso);
        }

        [TestMethod]
        public void TestMethodExcluirAlunoCurso()
        {
            var curso = RepositorioCurso.Consultar(1);
            var aluno = RepositorioAluno.Consultar(1);

            var alunoMatriculadoCurso = ServicoAluno.ExcluirAlunoCurso(aluno, curso);
            ServicoAluno.Salvar();

            Assert.IsNotNull(alunoMatriculadoCurso);
        }

        [TestMethod]
        public void TestMethodExcluirDisciplina()
        {
            var disciplina = RepositorioDisciplina.Consultar(1);
            
            Assert.ThrowsException<InvalidOperationException>(() => ServicoDisciplina.ExcluirDisciplina(disciplina));
        }


        [TestMethod]
        public void TestMethodExcluirDisciplinaCurso()
        {
            var curso = RepositorioCurso.Consultar(2);
            var disciplina = RepositorioDisciplina.Consultar(2);

            Assert.ThrowsException<InvalidOperationException>(() => ServicoDisciplina.ExcluirDisciplinaCurso(disciplina, curso));            
        }

        [TestMethod]
        public void TestMethodExcluirAluno()
        {
            var aluno = RepositorioAluno.Consultar(1);

            Assert.ThrowsException<InvalidOperationException>(() => ServicoAluno.ExcluirAluno(aluno));
        }

        [TestMethod]
        public void TestMethodEditarDisciplinaSemVinculo()
        {
            var disciplina = RepositorioDisciplina.Consultar(1);
            disciplina.Nome = "Cálculo I Alterado Segunda Vez";

            Assert.IsNotNull(ServicoDisciplina.EditarDisciplina(disciplina));            
        }

        [TestMethod]
        public void TestMethodEditarDisciplinaComVinculo()
        {
            var disciplina = RepositorioDisciplina.Consultar(2);
            disciplina.Nome = "Cálculo II Alterado";

            Assert.ThrowsException<InvalidOperationException>(() => ServicoDisciplina.EditarDisciplina(disciplina));
        }

    }
}
