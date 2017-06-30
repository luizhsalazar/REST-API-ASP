using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GestaoAlunos.Context;

namespace GestaoAlunos.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170627125727_MigracaoInicial")]
    partial class MigracaoInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GestaoAlunos.Models.Aluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Aluno");
                });

            modelBuilder.Entity("GestaoAlunos.Models.AlunoCurso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AlunoId");

                    b.Property<int>("CursoId");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.HasIndex("CursoId");

                    b.ToTable("AlunoCurso");
                });

            modelBuilder.Entity("GestaoAlunos.Models.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("GestaoAlunos.Models.CursoDisciplina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CursoId");

                    b.Property<int>("DisciplinaId");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.HasIndex("DisciplinaId");

                    b.ToTable("CursoDisciplina");
                });

            modelBuilder.Entity("GestaoAlunos.Models.Disciplina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Disciplina");
                });

            modelBuilder.Entity("GestaoAlunos.Models.AlunoCurso", b =>
                {
                    b.HasOne("GestaoAlunos.Models.Aluno", "Aluno")
                        .WithMany("AlunoCursos")
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestaoAlunos.Models.Curso", "Curso")
                        .WithMany("CursoAlunos")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestaoAlunos.Models.CursoDisciplina", b =>
                {
                    b.HasOne("GestaoAlunos.Models.Curso", "Curso")
                        .WithMany("CursoDisciplinas")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestaoAlunos.Models.Disciplina", "Disciplina")
                        .WithMany("DisciplinaCursos")
                        .HasForeignKey("DisciplinaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
