using GestaoAlunos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoAlunos.Interfaces
{
    public interface IServicoCurso
    {
        List<Curso> ListarCursos();

        List<Curso> ListarCursosComRelations();

        Curso Incluir(Curso curso);

        Curso Consultar(int id);

        Curso ExcluirCurso(Curso Curso);

        Curso ExcluirCursoCurso(Curso Curso, Curso curso);

        Curso MatricularCurso(Curso Curso);

        Curso MatricularCursoCurso(Curso Curso, Curso curso);

        Curso ExcluirCursoDisciplina(Curso Curso, Curso curso);

        void Salvar();
    }
}
