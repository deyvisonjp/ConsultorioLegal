using CL.Core.Domain;
using CL.Data.Context;
using CL.Manager.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        //Injeção de dependencia, ou seja atribuir a inicialização da classe via construtor (*Inversão de controle para desacoplar as aplicações*)
        private readonly ClContext _context;
        public ClienteRepository(ClContext context)
        {   
            _context = context;
        }

        //Acesso a base de dados POR isso Assincrono
        // Sempre que usar um assincrono utilize o retorn o Task
        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
            // Não vamos utilizar o tracking para ganho de perforamnce (tracking -> 'cache' dos dados)
            return await _context.Clientes.AsNoTracking().ToListAsync();
        }

        public async Task<Cliente> GetClienteAsync(int id)
        {
            return await _context.Clientes.FindAsync(id); //Mais recomendado para buscas por id
            // Todas outras formas possíveis
            //return await _context.Clientes.SingleOrDefaultAsync(cliente => cliente.Id == id); //Garante que exista somente Um (Nesse caso não precisamos, pois já declaramos isso no model)
            //return await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(cliente => cliente.Id == id);
            //return await _context.Clientes.AsNoTracking().Where(c => c.Id == id).FirstOrDefaultAsync();
        }

    }

}
