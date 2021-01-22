using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data.Context
{
    /// <summary>
    /// Context Class
    /// </summary>
    public class SmartContext : DbContext
    {
        public SmartContext(DbContextOptions<SmartContext> options) : base(options) { }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<AlunoCurso> AlunosCursos { get; set; }
        public DbSet<AlunoDisciplina> AlunosDisciplinas { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Professor> Professores { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AlunoDisciplina>()
                .HasKey(ad => new { ad.AlunoId, ad.DisciplinaId });
            builder.Entity<AlunoCurso>()
                .HasKey(ac => new { ac.AlunoId, ac.CursoId });

            builder.Entity<Professor>()
                .HasData(new List<Professor>(){
                    new Professor(1, "T001", "Lauro", "Silva", "(11) 9876-1234" ),
                    new Professor(2, "T002", "Roberto", "Santos", "(11) 9583-2222"),
                    new Professor(3, "T003", "Ronaldo", "Campos", "(11) 9345-2344"),
                    new Professor(4, "S001", "Rodrigo", "Alves", "(11) 9457-1256"),
                    new Professor(5, "S002", "Alexandre", "Montanha", "(11) 9874-2536"),
                            });

            builder.Entity<Curso>()
                .HasData(new List<Curso>{
                    new Curso(1, "Tecnologia da Informação"),
                    new Curso(2, "Sistema de Informação"),
                    new Curso(3, "Ciência da Computação")
                });

            // Esta forma documenta melhor o relacionamento
            builder.Entity<Disciplina>().HasData(
                    new Disciplina { Id = 1, Nome = "Matemática", ProfessorId = 1, CursoId = 3 },
                    new Disciplina { Id = 2, Nome = "Matemática", ProfessorId = 1, CursoId = 3 },
                    new Disciplina { Id = 3, Nome = "Física", ProfessorId = 2, CursoId = 3 },
                    new Disciplina { Id = 4, Nome = "Português", ProfessorId = 3, CursoId = 1 },
                    new Disciplina { Id = 5, Nome = "Inglês", ProfessorId = 4, CursoId = 1 },
                    new Disciplina { Id = 6, Nome = "Inglês", ProfessorId = 4, CursoId = 2 },
                    new Disciplina { Id = 7, Nome = "Inglês", ProfessorId = 4, CursoId = 3 },
                    new Disciplina { Id = 8, Nome = "Programação", ProfessorId = 5, CursoId = 1 },
                    new Disciplina { Id = 9, Nome = "Programação", ProfessorId = 5, CursoId = 2 },
                    new Disciplina { Id = 10, Nome = "Programação", ProfessorId = 5, CursoId = 3 }
                );

            builder.Entity<Aluno>()
                .HasData(new List<Aluno>(){
                    new Aluno(1, "2020-0001","Marta", "Kent", "33225555", DateTime.Parse("11/02/1980")),
                    new Aluno(2, "2020-0002","Paula", "Isabela", "3354288", DateTime.Parse("15/05/1965")),
                    new Aluno(3, "2020-0003","Laura", "Antonia", "55668899", DateTime.Parse("01/06/1978")),
                    new Aluno(4, "2020-0004","Luiza", "Maria", "6565659", DateTime.Parse("20/09/1980")),
                    new Aluno(5, "2020-0005","Lucas", "Machado", "565685415", DateTime.Parse("30/08/1988")),
                    new Aluno(6, "2020-0006","Pedro", "Alvares", "456454545", DateTime.Parse("22/10/1970")),
                    new Aluno(7, "2020-0007","Paulo", "José", "9874512", DateTime.Parse("08/07/1982"))
                });

            builder.Entity<AlunoDisciplina>()
                .HasData(new List<AlunoDisciplina>() {
                    new AlunoDisciplina() {AlunoId = 1, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 1, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 1, DisciplinaId = 5 },
                    new AlunoDisciplina() {AlunoId = 2, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 2, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 2, DisciplinaId = 5 },
                    new AlunoDisciplina() {AlunoId = 3, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 3, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 3, DisciplinaId = 3 },
                    new AlunoDisciplina() {AlunoId = 4, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 4, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 4, DisciplinaId = 5 },
                    new AlunoDisciplina() {AlunoId = 5, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 5, DisciplinaId = 5 },
                    new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 3 },
                    new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 3 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 5 }
                });
        }
    }
}
