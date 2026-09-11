# Estacionamento API

API REST para gestão de estacionamento, desenvolvida em **.NET 8** como projeto de portfólio. Ela permite cadastrar clientes, veículos e vagas, registrar a entrada e a saída de veículos e calcular o valor da permanência.

## Principais recursos

- Cadastro, consulta, listagem e remoção de clientes.
- Cadastro, consulta, listagem e remoção de veículos.
- Cadastro, consulta, listagem e remoção de vagas.
- Consulta de vagas disponíveis por tipo de veículo.
- Registro de entrada e saída de veículos.
- Validação de CPF, placa, número de vaga e identificadores obrigatórios.
- Tratamento global e padronizado de erros HTTP.
- Testes unitários de domínio, validações e regras de negócio.
- Documentação interativa com Swagger.

## Arquitetura

O projeto utiliza arquitetura em camadas para separar responsabilidades e aplicar princípios SOLID.

```text
Estacionamento.API
    Controllers, Swagger, middleware e injeção de dependência
            ↓
Estacionamento.Application
    Services, DTOs, validators e regras de casos de uso
            ↓
Estacionamento.Domain
    Entidades, enums e contratos (interfaces)
            ↓
Estacionamento.Infrastructure
    Entity Framework Core, SQL Server, migrations e repositórios
```

### Princípios aplicados

- **SRP**: controllers lidam com HTTP; services concentram regras de negócio; repositórios persistem dados.
- **DIP**: services dependem de interfaces, como `IVeiculoRepository`, e não de implementações concretas.
- **Injeção de dependência**: services, repositórios, validators e Unit of Work são registrados como `Scoped`.
- **Unit of Work**: operações de entrada e saída de veículo usam transação para manter vaga e estadia consistentes.

## Regras de negócio

- Um documento de cliente não pode ser duplicado.
- A placa de um veículo não pode ser duplicada.
- Uma vaga possui tipo compatível com `Carro` ou `Moto`.
- Uma vaga só pode receber veículo do mesmo tipo.
- Um veículo não pode possuir mais de uma estadia em aberto.
- Ao registrar a entrada, uma vaga disponível é marcada como ocupada.
- Ao finalizar a estadia, a vaga é liberada.
- O valor da estadia é de **R$ 10,00 por hora**, com cobrança mínima de uma hora.
- Criação de estadia e ocupação de vaga, assim como finalização e liberação, são confirmadas em uma única transação.

## Tecnologias

- .NET 8 / ASP.NET Core Web API
- Entity Framework Core 8
- SQL Server
- FluentValidation
- xUnit
- Swagger / OpenAPI

## Endpoints

| Recurso | Método | Rota | Descrição |
| --- | --- | --- | --- |
| Clientes | POST | `/api/clientes` | Cadastra um cliente |
| Clientes | GET | `/api/clientes` | Lista clientes |
| Clientes | GET | `/api/clientes/{id}` | Consulta um cliente |
| Clientes | DELETE | `/api/clientes/{id}` | Remove um cliente |
| Veículos | POST | `/api/veiculos` | Cadastra um veículo |
| Veículos | GET | `/api/veiculos` | Lista veículos |
| Veículos | GET | `/api/veiculos/{id}` | Consulta um veículo |
| Veículos | DELETE | `/api/veiculos/{id}` | Remove um veículo |
| Vagas | POST | `/api/vagas` | Cria uma vaga |
| Vagas | GET | `/api/vagas` | Lista vagas |
| Vagas | GET | `/api/vagas/numero/{numero}` | Consulta uma vaga pelo número |
| Vagas | GET | `/api/vagas/disponiveis?tipo=Carro` | Lista vagas disponíveis |
| Vagas | DELETE | `/api/vagas/{id}` | Remove uma vaga |
| Estadias | POST | `/api/estadias` | Registra a entrada de veículo |
| Estadias | GET | `/api/estadias` | Lista estadias |
| Estadias | GET | `/api/estadias/{id}` | Consulta uma estadia |
| Estadias | PATCH | `/api/estadias/{id}/finalizar` | Registra saída e calcula valor |

## Como executar localmente

### Pré-requisitos

- .NET SDK 8
- SQL Server ou SQL Server Express
- Visual Studio 2022, Visual Studio Code ou Rider

### 1. Configure a conexão

No arquivo `Estacionamento.API/appsettings.json`, ajuste a connection string para sua instância local de SQL Server:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=Estacionamento;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 2. Crie o banco e as tabelas

O projeto já contém a migration inicial. No **Package Manager Console** do Visual Studio, selecione `Estacionamento.Infrastructure` como projeto padrão e execute:

```powershell
Update-Database
```

### 3. Execute a API

```powershell
dotnet run --project Estacionamento.API
```

Abra a URL exibida no terminal e adicione `/swagger` para utilizar a documentação interativa.

## Testes

Os testes estão organizados por camada:

```text
Estacionamento.Tests
├── Domain       # Entidades e comportamentos de domínio
├── Validators   # Regras do FluentValidation
├── Services     # Regras de negócio
└── Fakes        # Repositórios em memória para testes isolados
```

Execute todos os testes com:

```powershell
dotnet test Estacionamento.Api.slnx
```

## Próximas evoluções

- Logs estruturados com Serilog e Seq.
- Docker Compose com API, SQL Server e Seq.
- Health checks e Redis.
- Autenticação JWT com perfis de usuário.
- Integração contínua com GitHub Actions.
- Nginx e load balancing para múltiplas instâncias da API.

