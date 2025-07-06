# Futebol Semanal API

## 1. Introdu��o

A **Futebol Semanal API** � uma API REST desenvolvida para auxiliar no gerenciamento de partidas, jogadores e estat�sticas do futebol amador (popularmente conhecido como "futebol de v�rzea"). Seu objetivo principal � fornecer uma ferramenta pr�tica e acess�vel para registrar informa��es como escala��es, desempenho de atletas e dados hist�ricos de partidas.

### Tecnologias Utilizadas

- **Backend**: .NET 6.0 (C#) com Entity Framework Core
- **Frontend**: React.js
- **Autentica��o**: JWT (JSON Web Token)
- **ORM**: Entity Framework Core
- **IDE**: Visual Studio 2022

### Reposit�rio do Projeto

[https://github.com/montwh1te/futebol-semanal\_with\_UI.git](https://github.com/montwh1te/futebol-semanal_with_UI.git)

---

## 2. Como Executar o Projeto (.NET 6.0)

1. Clone o reposit�rio:

   ```bash
   git clone https://github.com/montwh1te/futebol-semanal_with_UI.git
   cd futebol-semanal
   ```

2. Instale os pacotes necess�rios:

   ```bash
   dotnet restore
   ```

3. Configure o arquivo `appsettings.json` com a `ConnectionString` e `JwtSecretKey` e `DatabaseConnection`:

4. Aplique as migra��es do banco de dados:

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

## 3. Autentica��o JWT

A autentica��o da API � baseada em JWT (JSON Web Token). Ap�s um login bem-sucedido, o backend retorna um token JWT que deve ser enviado em todas as requisi��es autenticadas no cabe�alho HTTP:

**Formato:**

```
Authorization: Bearer <seu_token>
```


O token JWT cont�m informa��es do usu�rio (como ID, nome e e-mail) e uma data de expira��o. Ele � assinado com uma chave secreta definida no `appsettings.json` e validado em cada requisi��o protegida. Apenas as rotas de login e cadastro s�o p�blicas; todas as demais exigem autentica��o.

---

## 4. Upload de Imagem e Integra��o com Azure Blob Storage

A API permite o upload de imagens de jogadores e times via endpoints espec�ficos, utilizando o formato `multipart/form-data`. As imagens s�o armazenadas de forma segura no **Azure Blob Storage**, garantindo escalabilidade e alta disponibilidade.

- **Exemplo de requisi��o via Postman:**
  - **Endpoint:** `POST /api/Jogadores/{id}/upload-foto`
  - **Headers:**  
    `Authorization: Bearer <token>`
  - **Body (form-data):**
    - `foto`: [escolher arquivo]

A URL da imagem � salva no banco de dados e pode ser consumida pelo frontend.

---

## 5. Modelo de Dados e Relacionamentos

A aplica��o possui as seguintes entidades principais:

- **Usu�rio**
  - `Id`, `Nome`, `Email`, `Senha`, `Telefone`, `DataCadastro`, `UltimoLogin`, `Ativo`
- **Time**
  - `Id`, `Nome`, `CorUniforme`, `Cidade`, `Estado`, `Fundacao`, `FotoUrl`, `UsuarioId`
- **Jogador**
  - `Id`, `Nome`, `ImagemUrl`, `DataNascimento`, `Altura`, `Peso`, `Posicao`, `NumeroCamisa`, `TimeId`, `UsuarioId`
- **Partida**
  - `Id`, `Local`, `DataHora`, `CondicoesClimaticas`, `Campeonato`, `TimeCasaId`, `TimeVisitanteId`, `PlacarCasa`, `PlacarVisitante`
- **Estat�stica**
  - `Id`, `PartidaId`, `JogadorId`, `TimeId`, `Gols`, `Assistencias`, `Faltas`, `CartoesAmarelos`, `CartoesVermelhos`, `MinutosEmCampo`

### Relacionamentos

- Um **Usu�rio** pode cadastrar v�rios **Times** e **Jogadores**.
- Um **Time** pertence a um **Usu�rio** e pode ter v�rios **Jogadores**.
- Um **Jogador** pertence a um **Time** e a um **Usu�rio**.
- Uma **Partida** envolve dois **Times** (casa e visitante).
- Uma **Estat�stica** referencia um **Jogador**, uma **Partida** e um **Time**.

#### Modelo ER

> ![Modelo Entidade Relacionamento](./assets/Modelo_ER_FutebolSemanalAPI.png)

---

## 6. Backend

O backend da aplica��o foi desenvolvido em ASP.NET 6.0, utilizando Entity Framework Core para acesso a dados e integra��o com PostgreSQL. Todos os endpoints seguem o padr�o RESTful, com autentica��o JWT e integra��o nativa com Azure Blob Storage para upload de imagens.

---

## 7. Frontend

O frontend ser� desenvolvido em **React.js**, consumindo a API REST para exibir e gerenciar dados de usu�rios, times, jogadores, partidas e estat�sticas. O frontend ser� hospedado como aplica��o est�tica no Azure Static Web Apps, garantindo alta performance e integra��o cont�nua.

---

## 8. Estrutura de Deploy

- **Frontend (React):** Hospedado no servi�o **Azure Static Web Apps**.
- **Backend (ASP.NET 6.0):** Hospedado no servi�o **Azure App Service**.
- **Armazenamento de Imagens:** Azure Blob Storage.

Essa estrutura garante escalabilidade, seguran�a e facilidade de integra��o cont�nua via GitHub Actions ou Azure DevOps.

---

## 9. Licen�a

Este projeto est� licenciado sob a Licen�a MIT.

**Desenvolvido por:**  
Eduardo Monteblanco Matuella ([https://github.com/montwh1te](https://github.com/montwh1te))