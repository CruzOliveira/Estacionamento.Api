using Estacionamento.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.DTOs.Vaga
{
    public class CriarVagaRequest
    {
        public int Numero { get; set; }
        public TipoVeiculo Tipo { get; set; }
    }
}
