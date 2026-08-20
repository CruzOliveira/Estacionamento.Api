using Estacionamento.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Entities
{
    public class Veiculo
    {

        public Guid Id { get; private set; }

        public string Placa { get; private set; }

        public TipoVeiculo Tipo { get; private set; }

        public Guid ClienteId { get; private set; }

        public Cliente Cliente { get; private set; }

        private Veiculo() { }
        public Veiculo(string placa, TipoVeiculo tipo, Guid clienteId)
        {
            Id = Guid.NewGuid();
            Placa = placa;
            Tipo = tipo;
            ClienteId = clienteId;

        }
    }
}
