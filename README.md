# DengueAlert API

## Visão Geral
API para consulta de alertas de dengue, integrando com fonte externa e persistindo dados em banco MySQL. Desenvolvida como teste técnico.

## Tecnologias
- REST
- C# / .NET Core
- Entity Framework Core
- MySQL
- Docker (opcional para banco de dados)

## Pré-requisitos
- .NET 9.0 SDK
- MySQL ou Docker (para container MySQL)

## Configuração Rápida

### Banco de Dados
```

1. Configurar Banco de Dados com o Docker 
docker run --name dengue-db -e MYSQL_ROOT_PASSWORD=senha -e MYSQL_DATABASE=DengueDB -p 3306:3306 -d mysql:8.0
1.1 
Para fazer a migração na estrutura do banco:
dotnet ef database update
```
## Repositório
```

2. Clone o repositório:
git clone https://github.com/jhonvss1/DengueAlertApiBackkEnd
```

```

3. Acesse o diretório do projeto:
cd seu-repositorio
```
## Variáveis de ambiente
```

4. Configure as variáveis de ambiente do sistema necessárias.
Exemplos:
Nome Da Variável: DENGUEALERT_CONNECTION
Valor da Variável: Server=localhost;Port=3306; DataBase=DengueAlert; Uid=root; Pwd=;
```
Obs: Para fazer uma migração com o MySQL usando o Docker, seguimos com essa mesma variável de ambiente que é a string de conexão em que o MySQL usa por padrão.

```

5. Execute os seguintes comandos para restaurar as dependências e iniciar a API:
dotnet restore
dotnet run
```

```

6. Acesse a API em http://localhost:porta, onde "porta" é a porta configurada para a sua API.
````
## Endpoints

````
[GET] /api/Dengue
Descrição
Retorna todos os dados persistidos no banco de dados.

Exemplo de Resposta

[
  {
    "id": 3,
    "epidemologicalWeek": 202510,
    "year": 2025,
    "estimatedCases": 992,
    "reporteCases": 470,
    "alertLevel": 2,
    "createdAt": "2025-03-30T17:38:50.073959"
  }
]

[POST] /api/Dengue/fetch
Descrição: 
Busca dados da API externa e persiste no banco local.

[GET] /api/Dengue/{week}/{year}
Descrição:
Consulta dados por semana epidemiológica e ano.

Parâmetros
week (int): Número da semana (1-52)

year (int): Ano (ex: 2023)

Exemplo de Resposta:

{
  "id": 3,
  "epidemologicalWeek": 202510,
  "year": 2025,
  "estimatedCases": 992,
  "reporteCases": 470,
  "alertLevel": 2,
  "createdAt": "2025-03-30T17:38:50.073959"
}
````


