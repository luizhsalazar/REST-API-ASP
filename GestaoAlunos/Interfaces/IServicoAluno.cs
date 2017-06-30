using GestaoAlunos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoAlunos.Interfaces
{
    public interface IServicoAluno
    {
        List<Aluno> ListarAlunos();

        Aluno Consultar(int id);

        Aluno ExcluirAluno(Aluno aluno);

        Aluno ExcluirAlunoCurso(Aluno aluno, Curso curso);

        Aluno MatricularAluno(Aluno aluno);

        Aluno MatricularAlunoCurso(Aluno aluno, Curso curso);

        Aluno ExcluirAlunoDisciplina(Aluno aluno, Curso curso);

        void Salvar();
    }
}
