using Microsoft.AspNetCore.Mvc;
using CL.Core.Domain;
using CL.Manager.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CL.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private readonly IClienteManager _clienteManager;
        public ClientesController(IClienteManager clienteManager)
        {
            _clienteManager = clienteManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _clienteManager.GetClientesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _clienteManager.GetClienteAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Cliente cliente)
        {
            var clienteInserido = await _clienteManager.InsertClienteAsync(cliente);
            return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente); //Facilitador do .NET // nameof =  referencia auto o nome do metodo Get
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Cliente cliente)
        {
            var clienteAtualizado = await _clienteManager.UpdateClienteAsync(cliente);
            if (clienteAtualizado == null)
            {
                return NotFound(); //404
            }
            return Ok(clienteAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _clienteManager.DeleteClienteAsync(id);
            return NoContent();
        }
    }
}
