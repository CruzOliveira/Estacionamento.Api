using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.DTOs.Cliente
{
    public class CriarClienteRequest
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
    }
}
