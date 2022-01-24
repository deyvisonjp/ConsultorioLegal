using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL.Core.Shared.ModelView
{
    /// <summary>
    /// Objeto utilizado para inserção de um novo cliente
    /// </summary>
    public class NovoCliente
    {
        /// <summary>
        /// Nome do Cliente
        /// </summary>
        /// <example>Nome Sobrenome</example>
        public string Nome { get; set; }
        /// <summary>
        /// Data de nascimento do cliente
        /// </summary>
        /// <example>2000-12-31</example>
        public DateTime DataNascimento { get; set; }
        /// <summary>
        /// Sexo do cliente
        /// </summary>
        /// <example>M</example>
        public char Sexo { get; set; }
        /// <summary>
        /// Telefone do usuário
        /// </summary>
        /// <example>32112344321</example>
        public string Telefone { get; set; }
        /// <summary>
        /// Documento do cliente: CNH, CPF, RG
        /// </summary>
        /// <example>12345678911</example>
        public string Documento { get; set; }
    }
}
