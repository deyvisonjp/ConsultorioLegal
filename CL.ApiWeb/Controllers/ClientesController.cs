using Microsoft.AspNetCore.Mvc;
using CL.Core.Domain;
using CL.Manager.Interfaces;
using CL.Manager.Validator;
using CL.Core.Shared.ModelView;
using SerilogTimings;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CL.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private readonly IClienteManager _clienteManager;
        private readonly ILogger _logger;
        public ClientesController(IClienteManager clienteManager, ILogger<ClientesController> logger)
        {
            _clienteManager = clienteManager;
            _logger = logger;
        }

        /// <summary>
        /// Retorna todos os clientes cadastrados na base
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Cliente),StatusCodes.Status200OK)]//Antes da chamada ao URL mostra o que será retornado
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
                _logger.LogInformation("Foi requisitado um GET dos clientes.");
                return Ok(await _clienteManager.GetClientesAsync());
        }

        /// <summary>
        /// Retorna um cliente consultado por Id
        /// </summary>
        /// <param name="id" example="123">Id do cliente</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
                return Ok(await _clienteManager.GetClienteAsync(id));
        }

        /// <summary>
        /// Insere um novo cliente
        /// </summary>
        /// <param name="novoCliente"></param>
        [HttpPost]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(NovoCliente novoCliente)
        {
            _logger.LogInformation("Objeto recebido {@novoCliente}", novoCliente);

            Cliente clienteInserido;
            using (Operation.Time("Tempo de adição de um novo cliente."))
            {
                _logger.LogInformation("Foi requisitado a inserção de um novo cliente.");
                clienteInserido = await _clienteManager.InsertClienteAsync(novoCliente);
            }
            return CreatedAtAction(nameof(Get), new { id = clienteInserido.Id }, clienteInserido); //Facilitador do .NET // nameof =  referencia auto o nome do metodo Get
        }

        /// <summary>
        /// Atualiza cliente
        /// </summary>
        /// <param name="alteraCliente"></param>
        [HttpPut]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] AlteraCliente alteraCliente)
        {
            var clienteAtualizado = await _clienteManager.UpdateClienteAsync(alteraCliente);
            if (clienteAtualizado == null)
            {
                return NotFound(); //404
            }
            return Ok(clienteAtualizado);
        }

        /// <summary>
        /// Exclui um cliente
        /// </summary>
        /// <param name="id" example="123"></param>
        /// <remarks>Ao excluir um cliente o mesmo será removido permanentemente da base</remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await _clienteManager.DeleteClienteAsync(id);
            return NoContent();
        }
    }
}
