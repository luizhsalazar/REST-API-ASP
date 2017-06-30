using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoAlunos.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }

        public IList<CursoDisciplina> CursoDisciplinas { get; set; }

        public IList<AlunoCurso> CursoAlunos { get; set; }

        public Curso()
        {
            CursoDisciplinas = new List<CursoDisciplina>();
            CursoAlunos = new List<AlunoCurso>();
        }

    }
}
