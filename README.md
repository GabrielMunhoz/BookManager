# 📚 BookManager

**BookManager** é uma aplicação para gerenciamento de empréstimos de livros, desenvolvida com foco em arquitetura limpa, boas práticas de desenvolvimento e uso de tecnologias modernas.

## 🚀 Tecnologias Utilizadas

- .NET 6 / .NET 8
- ASP.NET Core
- Entity Framework Core
- AutoMapper
- Dapper
- CQRS
- DDD (Domain-Driven Design)
- RESTful APIs
- SQL Server
- Angular (para o front-end)
- Azure DevOps (CI/CD)

## 🧱 Arquitetura

O projeto segue os princípios da **Arquitetura Limpa**, separando responsabilidades em camadas distintas:

- **Domain**: Entidades e interfaces do domínio.
- **Application**: Casos de uso e lógica de negócio.
- **Infrastructure**: Acesso a dados e serviços externos.
- **Presentation**: Controllers e APIs.

## ⚙️ Como Executar

1. **Clone o repositório:**

   ```bash
   git clone https://github.com/GabrielMunhoz/BookManager.git
   ```

2. **Navegue até a pasta do projeto:**

   ```bash
   cd BookManager
   ```

3. **Configure o banco de dados:**

   - Certifique-se de que o SQL Server está instalado e em execução.
   - Atualize a string de conexão no arquivo `appsettings.json`.

4. **Execute as migrações:**

   ```bash
   dotnet ef database update
   ```

5. **Inicie a aplicação:**

   ```bash
   dotnet run
   ```

6. **Acesse a aplicação:**

   - API: `https://localhost:5001`
   - Front-end (Angular): `http://localhost:4200`

## 🧪 Testes

Para executar os testes:

```bash
dotnet test
```

## 🤝 Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests.

## 📄 Licença

Este projeto está licenciado sob a [MIT License](LICENSE).