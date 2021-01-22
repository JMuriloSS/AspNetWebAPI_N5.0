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
    /// Repositório do Aluno
    /// </summary>
    public class AlunoRepository : IAlunoRepository
    {
        private readonly SmartContext _context;

        public AlunoRepository(SmartContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Inserção de aluno
        /// </summary>
        /// <param name="aluno"></param>
        /// <returns></returns>
        public async Task<Aluno> InsertAlunoAsync(Aluno aluno)
        {
            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();
            return aluno;
        }

        /// <summary>
        /// Atualização do aluno
        /// </summary>
        /// <param name="aluno"></param>
        /// <returns></returns>
        public async Task<Aluno> UpdateAlunoAsync(Aluno aluno)
        {
            var alunoConsultado = await _context.Alunos
                                                 .FirstOrDefaultAsync(p => p.Id == aluno.Id);
            if (alunoConsultado == null)
            {
                return null;
            }
            _context.Entry(alunoConsultado).CurrentValues.SetValues(aluno);
            await _context.SaveChangesAsync();
            return alunoConsultado;
        }

        /// <summary>
        /// Deleta o registro do aluno
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Aluno> DeleteAlunoAsync(int id)
        {
            var alunoConsultado = await _context.Alunos.FindAsync(id);
            if (alunoConsultado == null)
            {
                return null;
            }
            var alunoRemovido = _context.Alunos.Remove(alunoConsultado);
            await _context.SaveChangesAsync();
            return alunoRemovido.Entity;
        }


        /// <summary>
        /// Repository - retorna dados de todos os alunos
        /// </summary>
        /// <param name="pageParams"></param>
        /// <param name="includeProfessor"></param>
        /// <returns></returns>
        public async Task<PageList<Aluno>> GetAllAlunosAsync(PageParamsAlunos pageParams, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                            .ThenInclude(ad => ad.Disciplina)
                            .ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(pageParams.Nome))
                query = query.Where(aluno => aluno.Nome
                                                .ToUpper()
                                                .Contains(pageParams.Nome.ToUpper()) ||
                                            aluno.Sobrenome
                                                .ToUpper()
                                                .Contains(pageParams.Nome.ToUpper()));

            if (!string.IsNullOrEmpty(pageParams.Matricula))
                query = query.Where(aluno => aluno.Matricula
                                                .ToUpper()
                                                .Contains(pageParams.Matricula.ToUpper()));
            if (pageParams.Ativo != null)
                query = query.Where(aluno => aluno.Ativo == (pageParams.Ativo != 0));

            //query = query.OrderByDescending(o => o.DataIni).ThenBy(o => o.Matricula);

            return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        /// <summary>
        /// Repository - retorna dados de todos os alunos e suas disciplinas
        /// </summary>
        /// <param name="disciplinaId"></param>
        /// <param name="includeProfessor"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Aluno>> GetAllAlunosByDisciplinaIdAsync(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                            .ThenInclude(ad => ad.Disciplina)
                            .ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking()
                        .OrderBy(a => a.Id)
                        .Where(aluno => aluno.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId));

            return await query.ToListAsync();

        }

        /// <summary>
        /// Repository - retorna dados de um aluno
        /// </summary>
        /// <param name="alunoId"></param>
        /// <param name="includeProfessor"></param>
        /// <returns></returns>
        public async Task<Aluno> GetAlunoByIdAsync(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                            .ThenInclude(ad => ad.Disciplina)
                            .ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking()
                        .OrderBy(a => a.Id)
                        .Where(aluno => aluno.Id == alunoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}