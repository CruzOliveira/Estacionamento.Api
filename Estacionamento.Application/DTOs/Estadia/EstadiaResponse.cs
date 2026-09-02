using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.DTOs.Estadia
{
    public class EstadiaResponse
    {
        public Guid Id { get; set; }
        public Guid VeiculoId { get; set; }
        public Guid VagaId { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime? Saida { get; set; }
        public decimal? Valor { get; set; }
    }
}
