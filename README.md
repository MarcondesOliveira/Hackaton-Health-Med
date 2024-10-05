# HACKATON - Health&Med

<div style="text-align: center;">
  <img src="https://img.shields.io/badge/.NET%208-333333?style=flat&logo=.net&logoColor=white" alt=".NET 8" />
  <img src="https://img.shields.io/badge/Docker-2496ED?style=flat&logo=docker&logoColor=white" alt="Docker" />
  <img src="https://img.shields.io/badge/SQL%20Server-CC2927?style=flat&logo=microsoftsqlserver&logoColor=white" alt="SQL Server" />
  <img src="https://img.shields.io/badge/Entity%20Framework-5C2D91?style=flat&logo=dotnet&logoColor=white" alt="Entity Framework" />
  <img src="https://img.shields.io/badge/MediatR-004B87?style=flat&logo=dotnet&logoColor=white" alt="MediatR" />
  <img src="https://img.shields.io/badge/JWT%20Bearer-000000?style=flat&logo=json-web-tokens&logoColor=white" alt="JwtBearer" />
  <img src="https://img.shields.io/badge/AutoMapper-003B57?style=flat&logo=dotnet&logoColor=white" alt="AutoMapper" />
  <img src="https://img.shields.io/badge/status-in_development-yellow" alt="Em Desenvolvimento" />
</div>

**Health&Med** é uma Operadora de Saúde que tem como objetivo digitalizar seus processos e operação. O principal gargalo da empresa é o Agendamento de Consultas Médicas, que atualmente ocorre exclusivamente através de ligações para a central de atendimento da empresa.

Recentemente, a empresa recebeu um aporte e decidiu investir no desenvolvimento de um sistema proprietário, visando proporcionar um processo de Agendamentos de Consultas Médicas 100% digital e mais ágil. 

## Funcionalidades

1. Introdução
Este documento descreve os requisitos funcionais do sistema de agendamento de consultas médicas, detalhando as funcionalidades que devem ser implementadas para atender às necessidades dos usuários, incluindo médicos e pacientes.

2. Requisitos Funcionais
2.1 Cadastro do Usuário (Médico)
- Descrição: O sistema deve permitir que o médico se cadastre.
- Campos obrigatórios:
    - Nome
    - CPF
    - Número do CRM
    - E-mail
    - Senha
    
2.2 Autenticação do Usuário (Médico)
- Descrição: O sistema deve permitir que o médico faça login.
- Credenciais:
    - E-mail
    - Senha
     
2.3 Cadastro/Edição de Horários Disponíveis (Médico)
- Descrição: O sistema deve permitir que o médico cadastre e edite seus dias e horários disponíveis para agendamento de consultas.

2.4 Cadastro do Usuário (Paciente)
- Descrição: O sistema deve permitir que o paciente se cadastre.
- Campos obrigatórios:
    - Nome
    - CPF
    - E-mail
    - Senha

2.5 Autenticação do Usuário (Paciente)
- Descrição: O sistema deve permitir que o paciente faça login.
- Credenciais:
    - E-mail
    - Senha

2.6 Busca por Médicos (Paciente)
- Descrição: O sistema deve permitir que o paciente visualize a lista de médicos disponíveis.

2.7 Agendamento de Consultas (Paciente)
- Descrição: O sistema deve permitir que o paciente agende consultas.
Funcionalidades:
    - Após selecionar o médico, o paciente deve visualizar os dias e horários disponíveis do médico.
    - O paciente poderá selecionar o horário de preferência e realizar o agendamento.

2.8 Notificação de Consulta Marcada (Médico)
- Descrição: Após o agendamento realizado pelo usuário paciente, o médico deverá receber uma notificação por e-mail.
- Conteúdo do E-mail:
    - Informações sobre a consulta, incluindo:
        - Nome do paciente
        - Data e horário da consulta
        - Nome do médico

## Tecnologias

- NET 8 - A plataforma para desenvolvimento da aplicação.
- Docker - Para containerização e execução consistente em diferentes ambientes.
- SQL Server - Banco de dados relacional para armazenamento de dados.
- Entity Framework - ORM para interação com o banco de dados.
- MediatR - Para a implementação de padrões CQRS e Mediator.
- JwtBearer - Para autenticação baseada em tokens JWT.
- AutoMapper - Para mapeamento entre objetos DTO e entidades.

## Instalação

**Pré-requisitos**
Certifique-se de ter o Docker e o .NET 8 instalados em seu sistema.

**Configuração**
- Num terminal digite: 

      git clone https://github.com/MarcondesOliveira/Hackaton-Health-Med.git
      cd Hackaton-Health-Med
      git checkout develop
      dotnet restore
      dotnet build
- Com o Docker Desktop em execução digite o comando para criar o banco pelo docker:
    
      docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<S3Nh4F0rT3>" -p 1433:1433 --name sqlserver-hackaton -h sqlserver-hackaton -d mcr.microsoft.com/mssql/server:2019-latest
- Abra a Solution no Visual Studio 2022 e habilite o projeto API como principal:
![StartupProject](https://github.com/MarcondesOliveira/Hackaton-Health-Med/blob/notificacao/Documentation/startupproject.png)
- Abra o Package Manager Console:
![PackageManagerConsole](https://github.com/MarcondesOliveira/Hackaton-Health-Med/blob/notificacao/Documentation/packagemanagerconsole.png)
- Em Default Project selecione o **Persistence** e rode os comandos:
![Persistence](https://github.com/MarcondesOliveira/Hackaton-Health-Med/blob/notificacao/Documentation/persistence.png)
        
        Add-Migration firstMigrationPersistence -Context HackatonDbContext
        Update-Database -Context HackatonDbContext


## Roteiro de uso da Api
**Autenticação**
    - Para usar o sistema, é necessário estar logado

        - Criar Médico / Paciente
        - Fazer login

**Médicos**
    - Cadastro

        {
            "nome": "Dr. João Silva",
            "cpf": "123.456.789-00",
            "crm": "123456-SP",
            "email": "email-valido@email.com", // email que vai receber a notificação
            "senha": "P@ssw0rd!2024"
        }

**Médicos**
    - Login
        
        {
          "email": "email-valido@email.com",
          "senha": "P@ssw0rd!2024"
        }

**Médicos**
    - Cadastrar consultas

        {
          "medicoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "data": "2024-10-05T14:36:21.768Z",
          "status": 0 // 0 Disponivel, 1 é Agendada e 2 é Atendida
        }

**Médicos**
    - Editar consultas
    
        {
          "consultaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "medicoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "status": 0,
          "data": "2024-10-05T14:37:52.338Z"
        }

**Pacientes**
    - Cadastro

        {
            "nome": "Marcondes Oliveira",
            "cpf": "00011116706",
            "email": "email@email.com",
            "senha": "lxdwu7#Ji["
        }

**Pacientes**
    - Login
    
        {
            "email": "email@email.com",
            "senha": "lxdwu7#Ji["
        }

**Pacientes**
    - Agendar consulta

        {
            "consultaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "medicoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "pacienteId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "data": "2024-10-05T14:41:51.152Z"
        }


## Consulta ao banco
**Como fazer consulta dos registros no banco de dados**
- Eu usei o Microsoft SQL Server Management com as seguintes configurações:
![ConsultaSQL](https://github.com/MarcondesOliveira/Hackaton-Health-Med/blob/notificacao/Documentation/consultasql.png)
   
- Mas qualquer Gerenciador de banco de dados poder ser utilizado

## License

MIT