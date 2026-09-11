using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Entities
{
    public class Estadia
    {
        public Guid Id { get; private set; }

        public Guid VeiculoId { get; private set; }

        public Guid VagaId { get; private set; }

        public DateTime Entrada { get; private set; }

        public DateTime? Saida { get; private set; }

        public decimal? Valor { get; private set; }

        public Veiculo Veiculo { get; private set; }

        public Vaga Vaga { get; private set; }

        private Estadia() { }
        public Estadia(
            Guid veiculoId, Guid vagaId, DateTime entrada
            )
        {
            Id = Guid.NewGuid();
            VeiculoId = veiculoId;
            VagaId = vagaId;
            Entrada = entrada;
            
        }

        public void Finalizar(DateTime saida, decimal valor)
        {
            if (Saida.HasValue)
            {
                throw new InvalidOperationException("Esta estadia já foi finalizada.");
            }

            if (saida < Entrada)
            {
                throw new ArgumentException("A saída não pode ser anterior à entrada.", nameof(saida));
            }

            Saida = saida;
            Valor = valor;
        }
    }
}
