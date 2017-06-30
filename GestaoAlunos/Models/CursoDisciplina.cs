using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoAlunos.Models
{
    public class CursoDisciplina
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public int DisciplinaId { get; set; }

        public Curso Curso { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}
