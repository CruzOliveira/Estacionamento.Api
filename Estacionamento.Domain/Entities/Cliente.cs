using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Entities
{
    public class Cliente
    {
        public Guid Id { get; private set; }

        public string Nome { get; private set; }

        public string Documento { get; private set; }

        public List<Veiculo> Veiculos { get; private set; }

        private Cliente()
        {
            Veiculos = new List<Veiculo>();
        }
        public Cliente(
            string nome, string documento, List<Veiculo> veiculos
            )
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Documento = documento;
            Veiculos = new List<Veiculo>();
        }
    }
}