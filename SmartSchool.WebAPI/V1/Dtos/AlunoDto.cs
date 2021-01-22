using System;

namespace SmartSchool.WebAPI.V1.Dtos
{
    /// <summary>
    /// Data Transfer Object - AlunoDto - V1
    /// </summary>
    public class AlunoDto
    {
        public int Id { get; set; }
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNasc { get; set; }
        public int Idade { get; set; }
        public DateTime DataIni { get; set; }
        public bool Ativo { get; set; }
    }
}