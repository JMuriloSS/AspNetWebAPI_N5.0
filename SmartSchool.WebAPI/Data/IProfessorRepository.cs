using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    /// <summary>
    /// Interface do repositório do professor
    /// </summary>
    public interface IProfessorRepository
    {
        Task<Professor> InsertProfessorAsync(Professor professor);
        Task<Professor> UpdateProfessorAsync(Professor professor);
        Task<Professor> DeleteProfessorAsync(int id);        

        Task<PageList<Professor>> GetAllProfessoresAsync(PageParamsProfessores pageParams, bool includeAlunos = false);
        Task<Professor> GetProfessorByIdAsync(int professorId, bool includeAlunos = false);
        Task<IEnumerable<Professor>> GetAllProfessoresByDisciplinaIdAsync(int disciplinaId, bool includeAluno = false);
        Task<IEnumerable<Professor>> GetProfessoresByAlunoIdAsync(int alunoId, bool includeAlunos = false);
    }
}