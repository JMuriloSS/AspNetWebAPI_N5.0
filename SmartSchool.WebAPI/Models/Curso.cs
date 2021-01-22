using System.Collections.Generic;

namespace SmartSchool.WebAPI.Models
{
    /// <summary>
    /// Entidade Curso
    /// </summary>
    public class Curso
    {
        public Curso() { }

        public Curso(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<Disciplina> Disciplinas { get; set; }
    }
}