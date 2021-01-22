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
    /// Versão 1: Controlador de Alunos
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly IAlunoRepository _repository;
        private readonly IMapper _mapper;
        /// <summary>
        /// Construtor para uso do repositório para AlunoController
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public AlunoController(IAlunoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Método para retornar dados de todos os alunos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParamsAlunos pageParams)
        {
            var alunos = await _repository.GetAllAlunosAsync(pageParams, true);

            var alunosResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);

            return Ok(alunosResult);
        }

        /// <summary>
        /// Método responsável por retonar apenas um único AlunoDTO.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ByDisciplina/{id}")]
        public async Task<IActionResult> GetByDisciplinaId(int id)
        {
            var result = await _repository.GetAllAlunosByDisciplinaIdAsync(id, false);
            return Ok(result);
        }

        /// <summary>
        /// Método responsável por retonar apenas um único AlunoDTO.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new AlunoRegistrarDto());
        }

        /// <summary>
        /// Retorna os dados de um aluno.
        /// </summary>
        /// <param name="id" example="1">Id do aluno.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var aluno = await _repository.GetAlunoByIdAsync(id, false);
            if (aluno == null) return NotFound("Aluno não encontrado!");

            var alunoDto = _mapper.Map<AlunoRegistrarDto>(aluno);

            return Ok(alunoDto);
        }

        /// <summary>
        /// Inserir registro de aluno
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            var alunoInserido = await _repository.InsertAlunoAsync(aluno);
            if (alunoInserido != null)
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno(a) não cadastrado(a)!");
        }

        /// <summary>
        /// Alterar o registro do aluno
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AlunoRegistrarDto model)
        {
            var aluno = await _repository.GetAlunoByIdAsync(id);

            if (aluno == null) return NotFound("Aluno não encontrado!");

            _mapper.Map(model, aluno);

            var alunoAlterado = await _repository.UpdateAlunoAsync(aluno);
            if (alunoAlterado != null)
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno(a) não alterado(a)!");
        }


        /// <summary>
        /// Alterar o registro do aluno de forma parcial
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, AlunoPatchDto model)
        {
            var aluno = await _repository.GetAlunoByIdAsync(id);

            if (aluno == null) return NotFound("Aluno não encontrado!");

            _mapper.Map(model, aluno);

            var alunoAtualizado = await _repository.UpdateAlunoAsync(aluno);
            if (alunoAtualizado != null)
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoPatchDto>(aluno));
            }
            return BadRequest("Aluno(a) não alterado(a)!");
        }

        /// <summary>
        /// api/aluno/{id}/trocarEstado - Alterar se o aluno está ativo ou não. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trocaEstado"></param>
        /// <returns></returns>
        [HttpPatch("{id}/trocarEstado")]
        public async Task<IActionResult> trocarEstado(int id, TrocaEstadoDto trocaEstado)
        {
            var aluno = await _repository.GetAlunoByIdAsync(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            aluno.Ativo = trocaEstado.Estado;

            var alunoAtualizado = await _repository.UpdateAlunoAsync(aluno);
            if (alunoAtualizado != null)
            {
                var msn = aluno.Ativo ? "ativado" : "desativado";
                return Ok(new { message = $"Aluno {msn} com sucesso!" });
            }
            return BadRequest("Aluno não Atualizado");
        }

        /// <summary>
        /// Deletar registro do aluno
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var alunoExcluido = await _repository.DeleteAlunoAsync(id);
            if (alunoExcluido != null)
            {
                return Ok("Aluno(a) deletado(a)!");
            }
            return BadRequest("Aluno(a) não deletado(a)!");
        }
    }
}