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

**Health&Med** � uma Operadora de Sa�de que tem como objetivo digitalizar seus processos e opera��o. O principal gargalo da empresa � o Agendamento de Consultas M�dicas, que atualmente ocorre exclusivamente atrav�s de liga��es para a central de atendimento da empresa.

Recentemente, a empresa recebeu um aporte e decidiu investir no desenvolvimento de um sistema propriet�rio, visando proporcionar um processo de Agendamentos de Consultas M�dicas 100% digital e mais �gil. 

## Funcionalidades

1. Introdu��o
Este documento descreve os requisitos funcionais do sistema de agendamento de consultas m�dicas, detalhando as funcionalidades que devem ser implementadas para atender �s necessidades dos usu�rios, incluindo m�dicos e pacientes.

2. Requisitos Funcionais
2.1 Cadastro do Usu�rio (M�dico)
- Descri��o: O sistema deve permitir que o m�dico se cadastre.
- Campos obrigat�rios:
    - Nome
    - CPF
    - N�mero do CRM
    - E-mail
    - Senha
    
2.2 Autentica��o do Usu�rio (M�dico)
- Descri��o: O sistema deve permitir que o m�dico fa�a login.
- Credenciais:
    - E-mail
    - Senha
     
2.3 Cadastro/Edi��o de Hor�rios Dispon�veis (M�dico)
- Descri��o: O sistema deve permitir que o m�dico cadastre e edite seus dias e hor�rios dispon�veis para agendamento de consultas.

2.4 Cadastro do Usu�rio (Paciente)
- Descri��o: O sistema deve permitir que o paciente se cadastre.
- Campos obrigat�rios:
    - Nome
    - CPF
    - E-mail
    - Senha

2.5 Autentica��o do Usu�rio (Paciente)
- Descri��o: O sistema deve permitir que o paciente fa�a login.
- Credenciais:
    - E-mail
    - Senha

2.6 Busca por M�dicos (Paciente)
- Descri��o: O sistema deve permitir que o paciente visualize a lista de m�dicos dispon�veis.

2.7 Agendamento de Consultas (Paciente)
- Descri��o: O sistema deve permitir que o paciente agende consultas.
Funcionalidades:
    - Ap�s selecionar o m�dico, o paciente deve visualizar os dias e hor�rios dispon�veis do m�dico.
    - O paciente poder� selecionar o hor�rio de prefer�ncia e realizar o agendamento.

2.8 Notifica��o de Consulta Marcada (M�dico)
- Descri��o: Ap�s o agendamento realizado pelo usu�rio paciente, o m�dico dever� receber uma notifica��o por e-mail.
- Conte�do do E-mail:
    - Informa��es sobre a consulta, incluindo:
        - Nome do paciente
        - Data e hor�rio da consulta
        - Nome do m�dico

## Tecnologias

- NET 8 - A plataforma para desenvolvimento da aplica��o.
- Docker - Para containeriza��o e execu��o consistente em diferentes ambientes.
- SQL Server - Banco de dados relacional para armazenamento de dados.
- Entity Framework - ORM para intera��o com o banco de dados.
- MediatR - Para a implementa��o de padr�es CQRS e Mediator.
- JwtBearer - Para autentica��o baseada em tokens JWT.
- AutoMapper - Para mapeamento entre objetos DTO e entidades.

## Instala��o

**Pr�-requisitos**
Certifique-se de ter o Docker e o .NET 8 instalados em seu sistema.

**Configura��o**
- Num terminal digite: 

      git clone https://github.com/MarcondesOliveira/Hackaton-Health-Med.git
      cd Hackaton-Health-Med
      git checkout develop
      dotnet restore
      dotnet build
- Com o Docker Desktop em execu��o digite o comando para criar o banco pelo docker:
    
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
**Autentica��o**
    - Para usar o sistema, � necess�rio estar logado

        - Criar M�dico / Paciente
        - Fazer login

**M�dicos**
    - Cadastro

        {
            "nome": "Dr. Jo�o Silva",
            "cpf": "123.456.789-00",
            "crm": "123456-SP",
            "email": "email-valido@email.com", // email que vai receber a notifica��o
            "senha": "P@ssw0rd!2024"
        }

**M�dicos**
    - Login
        
        {
          "email": "email-valido@email.com",
          "senha": "P@ssw0rd!2024"
        }

**M�dicos**
    - Cadastrar consultas

        {
          "medicoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "data": "2024-10-05T14:36:21.768Z",
          "status": 0 // 0 Disponivel, 1 � Agendada e 2 � Atendida
        }

**M�dicos**
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
- Eu usei o Microsoft SQL Server Management com as seguintes configura��es:
![ConsultaSQL](https://github.com/MarcondesOliveira/Hackaton-Health-Med/blob/notificacao/Documentation/consultasql.png)
   
- Mas qualquer Gerenciador de banco de dados poder ser utilizado

## License

MIT