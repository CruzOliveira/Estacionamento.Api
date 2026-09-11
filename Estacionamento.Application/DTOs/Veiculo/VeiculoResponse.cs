using Estacionamento.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.DTOs.Veiculo
{
    public class VeiculoResponse
    {
        public Guid Id { get; set; }
        public string Placa {  get; set; }
        public TipoVeiculo Tipo {  get; set; }
    }
}
