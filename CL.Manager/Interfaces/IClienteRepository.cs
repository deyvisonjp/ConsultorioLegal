﻿using CL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//MANAGER -> Regra de quem define como o repositório tem que funcionar

namespace CL.Manager.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente> GetClienteAsync(int id);
        Task<IEnumerable<Cliente>> GetClientesAsync();
    }
}