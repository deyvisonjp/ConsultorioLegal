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

## Aula 4 - 
https://youtu.be/U56ly54RT-I?list=PLbq2QKd5ieAt0H551D_0E4bGIYRxbq5HL

