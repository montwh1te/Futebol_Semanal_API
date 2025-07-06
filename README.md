# Futebol Semanal API

## 1. Introdução

A **Futebol Semanal API** é uma API REST desenvolvida para auxiliar no gerenciamento de partidas, jogadores e estatísticas do futebol amador (popularmente conhecido como "futebol de várzea"). Seu objetivo principal é fornecer uma ferramenta prática e acessível para registrar informações como escalações, desempenho de atletas e dados históricos de partidas.

### Tecnologias Utilizadas

- **Backend**: .NET 6.0 (C#) com Entity Framework Core
- **Frontend**: React.js
- **Autenticação**: JWT (JSON Web Token)
- **ORM**: Entity Framework Core
- **IDE**: Visual Studio 2022

### Repositório do Projeto

[https://github.com/montwh1te/futebol-semanal\_with\_UI.git](https://github.com/montwh1te/futebol-semanal_with_UI.git)

---

## 2. Como Executar o Projeto (.NET 6.0)

1. Clone o repositório:

   ```bash
   git clone https://github.com/montwh1te/futebol-semanal_with_UI.git
   cd futebol-semanal
   ```

2. Instale os pacotes necessários:

   ```bash
   dotnet restore
   ```

3. Configure o arquivo `appsettings.json` com a `ConnectionString` e `JwtSecretKey` e `DatabaseConnection`:

4. Aplique as migrações do banco de dados:

   ```bash
   dotnet ef database update
   ```

5. Rode o servidor (CTRL + F5):

   ```bash
   dotnet run
   ```

6. Acesse em:

   ```
   http://localhost:7000
   ```

---

## 3. Autenticação JWT

A autenticação da API é baseada em JWT (JSON Web Token). Após um login bem-sucedido, o backend retorna um token JWT que deve ser enviado em todas as requisições autenticadas no cabeçalho HTTP:

**Formato:**

```
Authorization: Bearer <seu_token>
```


O token JWT contém informações do usuário (como ID, nome e e-mail) e uma data de expiração. Ele é assinado com uma chave secreta definida no `appsettings.json` e validado em cada requisição protegida. Apenas as rotas de login e cadastro são públicas; todas as demais exigem autenticação.

---

## 4. Upload de Imagem e Integração com Azure Blob Storage

A API permite o upload de imagens de jogadores e times via endpoints específicos, utilizando o formato `multipart/form-data`. As imagens são armazenadas de forma segura no **Azure Blob Storage**, garantindo escalabilidade e alta disponibilidade.

- **Exemplo de requisição via Postman:**
  - **Endpoint:** `POST /api/Jogadores/{id}/upload-foto`
  - **Headers:**  
    `Authorization: Bearer <token>`
  - **Body (form-data):**
    - `foto`: [escolher arquivo]

A URL da imagem é salva no banco de dados e pode ser consumida pelo frontend.

---

## 5. Modelo de Dados e Relacionamentos

A aplicação possui as seguintes entidades principais:

- **Usuário**
  - `Id`, `Nome`, `Email`, `Senha`, `Telefone`, `DataCadastro`, `UltimoLogin`, `Ativo`
- **Time**
  - `Id`, `Nome`, `CorUniforme`, `Cidade`, `Estado`, `Fundacao`, `FotoUrl`, `UsuarioId`
- **Jogador**
  - `Id`, `Nome`, `ImagemUrl`, `DataNascimento`, `Altura`, `Peso`, `Posicao`, `NumeroCamisa`, `TimeId`, `UsuarioId`
- **Partida**
  - `Id`, `Local`, `DataHora`, `CondicoesClimaticas`, `Campeonato`, `TimeCasaId`, `TimeVisitanteId`, `PlacarCasa`, `PlacarVisitante`
- **Estatística**
  - `Id`, `PartidaId`, `JogadorId`, `TimeId`, `Gols`, `Assistencias`, `Faltas`, `CartoesAmarelos`, `CartoesVermelhos`, `MinutosEmCampo`

### Relacionamentos

- Um **Usuário** pode cadastrar vários **Times** e **Jogadores**.
- Um **Time** pertence a um **Usuário** e pode ter vários **Jogadores**.
- Um **Jogador** pertence a um **Time** e a um **Usuário**.
- Uma **Partida** envolve dois **Times** (casa e visitante).
- Uma **Estatística** referencia um **Jogador**, uma **Partida** e um **Time**.

#### Modelo ER

> ![Modelo Entidade Relacionamento](./assets/Modelo_ER_FutebolSemanalAPI.png)

---

## 6. Backend

O backend da aplicação foi desenvolvido em ASP.NET 6.0, utilizando Entity Framework Core para acesso a dados e integração com PostgreSQL. Todos os endpoints seguem o padrão RESTful, com autenticação JWT e integração nativa com Azure Blob Storage para upload de imagens.

---

## 7. Frontend

O frontend será desenvolvido em **React.js**, consumindo a API REST para exibir e gerenciar dados de usuários, times, jogadores, partidas e estatísticas. O frontend será hospedado como aplicação estática no Azure Static Web Apps, garantindo alta performance e integração contínua.

---

## 8. Estrutura de Deploy

- **Frontend (React):** Hospedado no serviço **Azure Static Web Apps**.
- **Backend (ASP.NET 6.0):** Hospedado no serviço **Azure App Service**.
- **Armazenamento de Imagens:** Azure Blob Storage.

Essa estrutura garante escalabilidade, segurança e facilidade de integração contínua via GitHub Actions ou Azure DevOps.

---

## 9. Licença

Este projeto está licenciado sob a Licença MIT.

**Desenvolvido por:**  
Eduardo Monteblanco Matuella ([https://github.com/montwh1te](https://github.com/montwh1te))