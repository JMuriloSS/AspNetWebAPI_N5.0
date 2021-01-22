## Udemy Course - Criando uma WebAPI RESTful utilizando Asp.NET Core 3.1, Entity Framework Core 3.1, Docker, Angular 10, MySQL e Mais!
## Prof. Vinícius de Andrade
# Criando um Web Api com ASP.Net Core - Conteúdo para estudos.

# AspNetCoreWebAPI N5.0

Optei também em fazer o curso na configuração abaixo:
- Debian 10/Linux
- AspNetCore 5.0
- AspNet CLI
- EF Core 5.0
- SwaggerUI
- SQLite
- VSCode com uso da maioria dos pacotes sugeridos pelo curso
- AutoMapper


SmartSchool.WebAPI - back-end
1 - Seções de 1 a 7:
    - Tipos dos campos (alteração livre)
        - Professor: string Registro;
        - Aluno: string Matricula;
        - Disciplina: double CargaHoraria;
        - AlunoCurso: double Nota;
        - AlunoDisciplina: double Nota;
        - Implementado CRUD para as entidades Aluno e Professore, testado no Swagger e Postman;
    - Falta implementar CRUD para as entidades Disciplina e Relacionamentos (fora do escopo do curso).

2 - Seção 8:
    - Conversão de todos os métodos para asyn/wait Aluno e Professor; (alteração livre)
    - Implementação de paginação também para o Professor; (alteração livre)
    - Aplicação de alguns conceitos conceitos do SOLID: (alteração livre)
        - Nas estruturas de algumas pastas;
        - Repositórios e Interfaces especificos para Aluno e Professor;
        - Parâmetros para paginação especificos para Aluno e Professor;
        - Injeção de Dependências (/Configuration) para os services e apps da Startup.cs

3 - Seção 9:
    - Sobre o docker, não inseri informações para usar o MySql no projeto, continuei usando o SQLite em todo o projeto;
    - Muito bom o conteúdo, exige um pouco mais de pesquisas e testes.

SmartschoolApp - front-end
4 - Seção 10:
    - Front-end com Angular 11, conforme o Professor falou, não é o foco do curso, um recurso apenas para apresentar e manipular os dados;
    - Falta implementar interfaces para as operações CRUD, create e delete das entidades Professor, Aluno e Disciplina (fora do escopo do curso).

        