# Ecommerce-API 🛒

Uma API RESTful de E-Commerce desenvolvida com ASP.NET Core, integrada com Entity Framework Core para gerenciamento de banco de dados. Este projeto foca em fornecer uma solução escalável e de fácil manutenção para o gerenciamento de produtos, pedidos, usuários, carrinho de compras e itens do carrinho.


## Descrição do Projeto
O Ecommerce-API é uma aplicação backend que fornece funcionalidades essenciais para a operação de uma plataforma de e-commerce. A API é construída utilizando ASP.NET Core e faz uso do Entity Framework Core para interagir com o banco de dados, garantindo um desempenho eficiente e a integridade dos dados.

## Funcionalidades 
- **Gerenciamento de Produtos:** Cadastro, atualização, remoção e listagem de produtos.
- **Gerenciamento de Pedidos:** Criação, acompanhamento e atualização do status de pedidos.
- **Gerenciamento de Usuários:** Cadastro, autenticação e gerenciamento de perfis de usuários.
- **Gerenciamento de Carrinho de Compras:** Adição, remoção e atualização de itens no carrinho.
- **Gerenciamento de Itens do Carrinho:** Controle detalhado de produtos dentro do carrinho, incluindo quantidades e preços.
- Integração com banco de dados relacional via Entity Framework Core.

## Tecnologias Utilizadas 🔧
- **ASP.NET Core** - Framework principal para o desenvolvimento da API.
- **Entity Framework Core** - Utilizado para a comunicação com o banco de dados.
- **SQL Server** - Opção de bancos de dados utilizado.
- **Swagger** - Documentação interativa da API.
- **Git** - Controle de versão do projeto.

## Padrões de Projeto
- **Repository Pattern** - Para encapsular a lógica de acesso aos dados.
- **Dependency Injection** - Para gestão de dependências e inversão de controle.
- **DTOs (Data Transfer Objects)** - Para organizar a transferência de dados entre camadas.
- **RESTful Principles** - Estrutura das rotas e operações da API seguem os princípios RESTful.

## Instalação
Siga os passos abaixo para configurar o projeto localmente:

1. Clone o repositório:
    ```bash
    git clone https://github.com/MurilloLS/Ecommerce-api.git
    ```
2. Navegue para o diretório do projeto:
    ```bash
    cd Ecommerce-api
    ```
3. Restaure as dependências do projeto:
    ```bash
    dotnet restore
    ```
4. Configure a string de conexão com o banco de dados no arquivo `appsettings.json`.
5. Aplique as migrações do Entity Framework para configurar o banco de dados:
    ```bash
    dotnet ef database update
    ```
6. Execute o projeto:
    ```bash
    dotnet run
    ```

## Uso
Após a instalação, a API estará disponível para uso em `http://localhost:5000`. Utilize o Swagger para explorar e testar os endpoints disponíveis.

## Contribuições
Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou enviar pull requests. Para grandes mudanças, por favor, abra uma discussão primeiro para debatermos o que você gostaria de alterar.

## Contato
Murillo Santos - [LinkedIn](https://linkedin.com/in/murillo-santos1)
