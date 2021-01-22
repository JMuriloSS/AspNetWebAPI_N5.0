using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data.Context;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    /// <summary>
    /// Repository Class
    /// </summary>
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly SmartContext _context;

        public ProfessorRepository(SmartContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Insere professor
        /// </summary>
        /// <param name="professor"></param>
        /// <returns></returns>
        public async Task<Professor> InsertProfessorAsync(Professor professor)
        {
            await _context.Professores.AddAsync(professor);
            await _context.SaveChangesAsync();
            return professor;
        }

        public async Task<Professor> UpdateProfessorAsync(Professor professor)
        {
            var professorConsultado = await _context.Professores
                                                 .FirstOrDefaultAsync(p => p.Id == professor.Id);
            if (professorConsultado == null)
            {
                return null;
            }
            _context.Entry(professorConsultado).CurrentValues.SetValues(professor);
            await _context.SaveChangesAsync();
            return professorConsultado;
        }

        /// <summary>
        /// Deleta professor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Professor> DeleteProfessorAsync(int id)
        {
            var professorConsultado = await _context.Professores.FindAsync(id);
            if (professorConsultado == null)
            {
                return null;
            }
            var professorRemovido = _context.Professores.Remove(professorConsultado);
            await _context.SaveChangesAsync();
            return professorRemovido.Entity;
        }


        public async Task<PageList<Professor>> GetAllProfessoresAsync(PageParamsProfessores pageParams, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos)
            {
                query = query.Include(p => p.Disciplinas)
                            .ThenInclude(d => d.AlunosDisciplinas)
                            .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);


            query = query.AsNoTracking().OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(pageParams.Nome))
                query = query.Where(professor => professor.Nome
                                                .ToUpper()
                                                .Contains(pageParams.Nome.ToUpper()) ||
                                            professor.Sobrenome
                                                .ToUpper()
                                                .Contains(pageParams.Nome.ToUpper()));

            if (!string.IsNullOrEmpty(pageParams.Registro))
                query = query.Where(professor => professor.Registro
                                                .ToUpper()
                                                .Contains(pageParams.Registro.ToUpper()));
            if (pageParams.Ativo != null)
                query = query.Where(professor => professor.Ativo == (pageParams.Ativo != 0));

            // query = query.OrderByDescending(o => o.DataIni).ThenBy(o => o.Registro);
        
            return await PageList<Professor>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<IEnumerable<Professor>> GetAllProfessoresByDisciplinaIdAsync(int disciplinaId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos)
            {
                query = query.Include(d => d.Disciplinas)
                            .ThenInclude(ad => ad.AlunosDisciplinas)
                            .ThenInclude(a => a.Aluno);
            }

            query = query.AsNoTracking()
                        .OrderBy(aluno => aluno.Id)
                        .Where(aluno => aluno.Disciplinas
                        .Any(d => d.AlunosDisciplinas
                        .Any(ad => ad.DisciplinaId == disciplinaId)));

            return await query.ToListAsync();

        }

        public async Task<Professor> GetProfessorByIdAsync(int professorId, bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(p => p.Disciplinas)
                            .ThenInclude(d => d.AlunosDisciplinas)
                            .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()
                        .OrderBy(a => a.Id)
                        .Where(professor => professor.Id == professorId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Professor>> GetProfessoresByAlunoIdAsync(int alunoId, 
            bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(aluno => aluno.Disciplinas.Any(
                             d => d.AlunosDisciplinas.Any(ad => ad.AlunoId == alunoId)
                         ));

            return await query.ToListAsync();
        }
    }
}