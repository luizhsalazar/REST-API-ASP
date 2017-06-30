using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoAlunos.Models
{
    public class Disciplina
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }

        public IList<CursoDisciplina> DisciplinaCursos { get; set; }
    }
}
