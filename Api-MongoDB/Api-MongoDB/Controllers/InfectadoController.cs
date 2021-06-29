using Api_MongoDB.Data.Collections;
using Api_MongoDB.Models;
using Api_MongoDB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.Annotations;

namespace Api_MongoDB.Controllers
{
    [Route("api/infectado")]
    [ApiController]
    [Authorize]
    public class InfectadoController : ControllerBase
    {
        private readonly InfectadoService _service;

        public InfectadoController(InfectadoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Este serviço permite salvar dados de um infectado pela covid-19.
        /// </summary>
        /// <param name="InfectadoDto"></param>
        /// <returns>Retorna 201 e dados do infectado salvo</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao salvar dados de um infectado")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] InfectadoDto dto)
        {
            var infectado = _service.Salvar(dto);

            return CreatedAtRoute("ObterInfectado", new { id = infectado.Id }, infectado);
        }

        /// <summary>
        /// Este serviço permite obter dados de um infectado pela covid-19.
        /// </summary>        
        /// <returns>Retorna 200 e lista de dados dos infectados salvos</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter a lista de dados dos infectados")]
        [SwaggerResponse(statusCode: 404, description: "Dados do infectado não encontrado")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        public ActionResult ObterInfectados()
        {
            var infectados = _service.Obter();

            return Ok(infectados);
        }

        /// <summary>
        /// Este serviço permite obter dados de um infectado pela covid-19.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna 200 e dados do infectado salvo</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter dados de um infectado")]
        [SwaggerResponse(statusCode: 404, description: "Dados do infectado não encontrado")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet("{id:length(24)}", Name = "ObterInfectado")]
        public ActionResult ObterInfectado([FromRoute] string id)
        {
            var infectado = _service.Obter(id);

            if (infectado == null)
            {
                return NotFound();
            }
            return Ok(infectado);
        }

        /// <summary>
        /// Este serviço permite atulizar dados de um infectado pela covid-19.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna 204 </returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao alterar dados de um infectado")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPut("{id:length(24)}")]
        public ActionResult AtualizarInfectado([FromRoute] string id,[FromBody] InfectadoDto dto)
        {

            _service.Atualizar(id, dto);

            return NoContent();
        }


        /// <summary>
        /// Este serviço permite excluir dados de um infectado pela covid-19.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna 204 </returns>
        [SwaggerResponse(statusCode: 204, description: "Sucesso ao excluir dados de um infectado")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpDelete("{id:length(24)}")]
        public ActionResult ExcluirInfectado([FromRoute] string id)
        {
            _service.Excluir(id);

            return NoContent();
        }
    }
}
