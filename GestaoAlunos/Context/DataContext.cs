using GestaoAlunos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoAlunos.Context
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database=GestaoAlunosCurso; Trusted_Connection=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlunoCurso>(ac =>
            {
                ac.HasOne(aluno => aluno.Aluno)
                .WithMany(alunoCurso => alunoCurso.AlunoCursos)
                .HasForeignKey(pk => pk.AlunoId);
            });

            modelBuilder.Entity<Aluno>(aluno =>
            {
                aluno.HasMany(a => a.AlunoCursos)
                .WithOne(ac => ac.Aluno)
                .HasForeignKey(pk => pk.AlunoId);
            });

            modelBuilder.Entity<CursoDisciplina>(cd =>
            {
                cd.HasOne(curso => curso.Curso)
                .WithMany(cursoDisciplina => cursoDisciplina.CursoDisciplinas)
                .HasForeignKey(pk => pk.CursoId);
            });
        }
    }
}
