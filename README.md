# Ecommerce-API 🛒

Uma API RESTful poderosa e flexível desenvolvida com **ASP.NET Core** e **Entity Framework Core** para atender às necessidades de uma plataforma de e-commerce. Esta API facilita o gerenciamento de produtos, pedidos, usuários, carrinho de compras e itens do carrinho com uma arquitetura escalável e bem organizada.

## 🚀 Visão Geral

O **Ecommerce-API** oferece uma base sólida para construir e expandir soluções de e-commerce. Com uma abordagem moderna utilizando **ASP.NET Core** para a lógica de aplicação e **Entity Framework Core** para o gerenciamento de dados, a API é ideal para desenvolver sistemas de e-commerce eficientes e de alto desempenho.

## 🌟 Funcionalidades Principais

- **Gerenciamento de Produtos:** Crie, atualize, exclua e liste produtos disponíveis para venda.
- **Gerenciamento de Pedidos:** Crie novos pedidos, acompanhe seu status e atualize informações conforme necessário.
- **Gerenciamento de Usuários:** Gerencie o cadastro, autenticação e perfil dos usuários da plataforma.
- **Carrinho de Compras:** Adicione, remova e atualize itens no carrinho de compras dos usuários.
- **Itens do Carrinho:** Controle detalhado dos produtos no carrinho, incluindo ajuste de quantidades e preços.

## 🔧 Tecnologias Utilizadas

- **ASP.NET Core:** Framework de desenvolvimento para criar a API.
- **Entity Framework Core:** ORM para interagir com o banco de dados de forma eficiente.
- **SQL Server:** Banco de dados relacional utilizado para armazenar os dados.
- **Swagger:** Interface para documentação e teste interativo da API.
- **Git:** Controle de versão do projeto.

## 🛠️ Padrões de Projeto

- **Repository Pattern:** Organização da lógica de acesso aos dados, promovendo a separação de responsabilidades.
- **Dependency Injection:** Gerenciamento das dependências e implementação da inversão de controle.
- **DTOs (Data Transfer Objects):** Estrutura para a transferência eficiente de dados entre camadas da aplicação.
- **RESTful Principles:** Estrutura de rotas e operações seguindo as melhores práticas REST.

## ⚙️ Instalação e Configuração

Siga os passos abaixo para configurar o projeto localmente:

1. **Clone o Repositório:**
    ```bash
    git clone https://github.com/MurilloLS/Ecommerce-api.git
    ```
2. **Navegue até o Diretório do Projeto:**
    ```bash
    cd Ecommerce-api
    ```
3. **Restaure as Dependências do Projeto:**
    ```bash
    dotnet restore
    ```
4. **Configure a String de Conexão:**
   - Edite o arquivo `appsettings.json` para configurar a conexão com o banco de dados.

5. **Aplique as Migrações do Banco de Dados:**
    ```bash
    dotnet ef database update
    ```
6. **Execute a Aplicação:**
    ```bash
    dotnet run
    ```

## 🔍 Uso da API

Após a configuração, a API estará acessível em `http://localhost:5000`. Use o Swagger para explorar os endpoints disponíveis e testar as funcionalidades da API.

## 💬 Contribuições

Contribuições são bem-vindas! Se você deseja contribuir para o projeto, sinta-se à vontade para abrir issues ou enviar pull requests. Para alterações significativas, abra uma discussão para alinharmos as mudanças desejadas.

## 📫 Contato

Murilo Santos - [LinkedIn](https://linkedin.com/in/murillo-santos1)
