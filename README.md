# Projeto Consultorio Legal, aprendizado com 
https://youtu.be/ADU0R7Pjwzc?list=PLbq2QKd5ieAt0H551D_0E4bGIYRxbq5HL

> Projeto contém
- Api Core
- Bibliotecas
- Teste do xUnit

## Estrutura Backend
- WebAPi: Aplicação API
- Manager: Regras de negócio
- Core: Modelo (Ainda será subdividida)
- Data: Responsável pela persistencia de dados, onde vai ficar a ORM.

## Pacotes instalados

### WebApi com Swagger
- Swashbucle.AspNetCore (EM WebAPI)

### Configurar EntityFramework Core
https://youtu.be/v-Qc_ek-gwE?list=PLbq2QKd5ieAt0H551D_0E4bGIYRxbq5HL
- install-package Microsoft.EntityFrameworkCore (Em WebApi e Data)
- install-package Microsoft.EntityFrameworkCore.Design
- install-package Microsoft.EntityFrameworkCore.Tools
- install-package Microsoft.EntityFrameworkCore.SqlServer

### Em Data 
- add-migration inicial (Cria as migrations)
- update-database (Executas as migrations criando a(s) tabela(s) no banco)

## Aula 3 -Repository
https://youtu.be/ADU0R7Pjwzc?list=PLbq2QKd5ieAt0H551D_0E4bGIYRxbq5HL
- Primeiro Repository Criado: ClienteRepository.cs
	- Vamos utilizar a Injeção de Dependencia que é uma ferramenta para Inversão de controle(SOLID), 
		que serve para desacoplar as aplicações (Nada mais é que atribuir a iniciação de uma classe via construtor)
	- Anotações iniciais anotadas nos métodos da classe ClienteRepository
	- Devemos criar em Manager (regras/o 'contrato'): IClienteRepository 
	- Criação da IClienteManager : Que é responsavél por chamar o repositorio
	- Criação da Clientemanager em Implementation
- Em Program.cs: adicionar as linhas:
	1. builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
	2. builder.Services.AddScoped<IClienteManager, ClienteManager>();

- EM RESUMO:
	1. Criamos o Repository
	2. Criamos a Interface de Repository
	3. Apontamos O repository para manager (e criamos a Interface de manager)
	4. Add o escopos nos Program.cs
	5. utlizamos a manager em Controller

## Aula 4 - Post, Put e Delete
https://youtu.be/U56ly54RT-I?list=PLbq2QKd5ieAt0H551D_0E4bGIYRxbq5HL
1. Implementar as tres ações em Repository
2. Implementar as interfaces (IClienteRepository, IClienteManager)
3. Implementar a Manager
4. Implementar no Usuario Controller 

## Aula 5 - Fluent Validation
- Vamos validar os dados antes de serem inseridos na base de dados

- Regra de negócio Validator em Manager
- Vamos usar uma ferramenta para nos auxiliar com as validações
	1. Instalar o pacote na camada manager e também em API
		- install-package FluentValidation.AspNetCore
		- vamos criar mais campos no dmain cliente
		- com a criação dos novos campos precisamos (DATA) 'add-migration adicionaCamposCliente' e 'update-database'
	2. Validação:
		- RuleFor(RegraPara) 
	3. Configurar a injeção de dependencia
		- Adicionar no Startup(Program) o controller: .AddFluentValidation(p => p.RegisterValidatorsFromAssemblyContaining<ClienteValidator>())

## Aula 6 - Model View - AutoMapper
https://youtu.be/jYTPRki83z0?list=PLbq2QKd5ieAt0H551D_0E4bGIYRxbq5HL
- Divisão de responsabilidades (Model View/DTO)
- Instalação do pacote AutoMapper no projeto 
	- API: install-package automapper e install-package automapper.extensions.microsoft.dependencyinjection
	- MANAGER: somente... install-package automapper
- Vamos criar uma represetação do que precisamos receber do usuário que esta recebendo a API
- Na pasta 04.CL.Core Adicionar um novo projeto Class Library (.NET Standard) com o nome NovoCliente
	- Essa classe será intermediária, aqui passa o que realmente a API espera receber do usuário
- No Projeto manager vamos criar a pasta appings com o arquivo NovoClienteMappingProfile
- Adicionar o serviço de automapper em Program/Startup

## Aula 7 - Organizando Startup/Program

## Aula 8 - Swagger
Devido a importancia da documentação, devemos usar todos recursos possíveis que o Swagger nos concede
- Em configurações da API
	- Build/Criar:
		- Gerar arquivo de documentação Xml
		- Suprimir avisos específicos: 1591
- O mesmo vai acontecer para os atributos do Cliente (NovoCliente)
	- Configurar CL.Core.Shared
		- Gerar o arquivo xml: CL.Core.Shared.xml dentro do projeto API

## Aula 9 - Swagger com FluentValidation
- Dentro do projeto CL.WebApi
	- install-package MicroElements.Swashbuckle.FluentValidation

## Aula 10 - EF Core - Garantindo integridade estrutural
_Nossa aplicação é CodeFirst_
Criando a DatabaseConfig, com isso automatizamos a criação do banco (update-migration)

