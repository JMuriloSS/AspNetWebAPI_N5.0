using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    /// <summary>
    /// Interface do Repositório de Aluno
    /// </summary>
    public interface IAlunoRepository
    {
        Task<Aluno> InsertAlunoAsync(Aluno aluno);
        Task<Aluno> UpdateAlunoAsync(Aluno aluno);
        Task<Aluno> DeleteAlunoAsync(int id);
        Task<PageList<Aluno>> GetAllAlunosAsync(PageParamsAlunos pageParams, bool includeProfessor = false);
        Task<IEnumerable<Aluno>> GetAllAlunosByDisciplinaIdAsync(int disciplinaId, bool includeProfessor = false);
        Task<Aluno> GetAlunoByIdAsync(int alunoId, bool includeProfessor = false);
        
    }
}