using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GestaoAlunos.Models
{
    public class AlunoCurso
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int CursoId { get; set; }

        public Aluno Aluno { get; set; }
        public Curso Curso { get; set; }
    }
}
