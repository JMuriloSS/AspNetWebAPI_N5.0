using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V1.Dtos;
using SmartSchool.WebAPI.Models;
using System.Threading.Tasks;
using SmartSchool.WebAPI.Helpers;

namespace SmartSchool.WebAPI.V1.Controllers
{
    /// <summary>
    /// Versão 1: Controlador de Professores
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        public readonly IProfessorRepository _repository;
        private readonly IMapper _mapper;
        public ProfessorController(IProfessorRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Retorna dados de todos os professores
        /// </summary>
        /// <param name="pageParams"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParamsProfessores pageParams)
        {
            var professores = await _repository.GetAllProfessoresAsync(pageParams, true);

            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }

        /// <summary>
        /// Retorna dados de um professor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var professor = await _repository.GetProfessorByIdAsync(id, true);
            if (professor == null) return NotFound("Professor não encontrado!");

            var professorDto = _mapper.Map<ProfessorDto>(professor);

            return Ok(professorDto);
        }


        /// <summary>
        /// Retorna dados dos alunos vinculados ao professor
        /// </summary>
        /// <param name="alunoId"></param>
        /// <returns></returns>
        [HttpGet("byaluno/{alunoId}")]
        public async Task<IActionResult> GetByAlunoIdAsync(int alunoId)
        {
            var professores = await _repository.GetProfessoresByAlunoIdAsync(alunoId, true);
            if (professores == null) return BadRequest("Professores não encontrados");

            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }


        /// <summary>
        /// Inserir registro de professor
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);

            var professorInserido = await _repository.InsertProfessorAsync(professor);
            if (professorInserido != null)
            {
                return Created($"/api/professor/{model.Id}",
                    _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor(a) não cadastrado(a)!");
        }

        /// <summary>
        /// Alterar registro do professor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProfessorRegistrarDto model)
        {
            var professor = await _repository.GetProfessorByIdAsync(id, false);

            if (professor == null) return NotFound("Professor não encontrado!");

            _mapper.Map(model, professor);

            var professorAtualizado = await _repository.UpdateProfessorAsync(professor);
            if (professorAtualizado != null)
            {
                return Created($"/api/professor/{model.Id}",
                   _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor(a) não alterado(a)!");
        }

        /// <summary>
        /// Alterar registro do professor de forma parcial
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, ProfessorRegistrarDto model)
        {
            var professor = await _repository.GetProfessorByIdAsync(id, false);
            if (professor == null) return NotFound("Professor não encontrado!");

            _mapper.Map(model, professor);

            var professorAlterado = _repository.UpdateProfessorAsync(professor);
            if (professorAlterado != null)
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor(a) não alterado(a)!");
        }

        /// <summary>
        /// Deleta o registro do professor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var professorExcluido = await _repository.DeleteProfessorAsync(id);
            if (professorExcluido != null)
            {
                return Ok("Professor(a) deletado(a)!");
            }
            return BadRequest("Professor(a) não deletado(a)!");
        }
    }
}