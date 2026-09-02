using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.DTOs.Vaga
{
    public class VagaResponse
    {
        public Guid Id { get; set; }
        public int Numero { get; set; }
        public string Tipo { get; set; }
        public bool Ocupada { get; set; }
    }
}
