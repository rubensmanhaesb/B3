################################################################################
# B3 - Simulador de CDB
################################################################################

Projeto desenvolvido como parte de um desafio técnico da Diretoria de Desenvolvimento de Sistemas de Middle e Back Office, com foco em análise, design SOLID, testes e desempenho.

################################################################################
# Descrição
################################################################################

O sistema permite simular investimentos em CDB com base em valor inicial e prazo. Os resultados incluem valor bruto e líquido, considerando alíquota regressiva de imposto. Ambos os projetos estão em uma única solução no Visual Studio
 (\B3\B3.API\B3.API.sln)

Dois componentes principais compõem a solução:
- **Frontend:** Aplicação Angular CLI responsiva e intuitiva.
- **Backend:** API REST em ASP.NET Core 9.0 para cálculo do CDB.

A aplicação é 100% **containerizada com Docker**.

################################################################################
# Tecnologias Utilizadas
################################################################################

- .NET 9.0 (ASP.NET Core)
- Angular 17 (Angular CLI)
- Docker & Docker Compose
- Swagger (Swashbuckle.AspNetCore)
- FluentValidation
- xUnit + Coverlet + coverlet.collector (para testes e cobertura)

################################################################################

CI/CD e Publicação
################################################################################

O projeto conta com pipeline de **CI/CD** automatizado utilizando **GitHub Actions**, incluindo as etapas de build, testes, cobertura e deploy automatizado para o Azure App Service.

A aplicação está publicada e disponível nos seguintes ambientes:

- **Produção**: 🌐 https://b3.azurewebsites.net/  
- **Teste (QA)**: 🌐 https://b3qa.azurewebsites.net/

A publicação em produção é realizada **somente após aprovação manual**, garantindo que todas as validações no ambiente de teste tenham sido concluídas com sucesso.

<img width="1287" height="190" alt="image" src="https://github.com/user-attachments/assets/79ea2c23-d5c3-4742-9bc9-b6bf15558f09" />


################################################################################
# Executando com Docker
################################################################################

Pré-requisitos: Docker e Docker Compose instalados.

1. Clone este repositório:
    git clone https://github.com/rubensmanhaesb/B3.git

2. Navegue até a pasta do projeto:
    cd B3

3. Execute o Docker Compose:
    docker-compose up --build  -d

4. Acesse os endpoints:
    - Frontend Angular: http://localhost:4200/
    - API Swagger:     http://localhost:8080/swagger/index.html

Obs.: O Angular se conecta à API diretamente entre os containers via rede bridge.

################################################################################
# Testes e Cobertura
################################################################################

A cobertura total obtida foi de:

- **Linhas cobertas:** 98%
- **Branches cobertos:** 100%

O relatório de cobertura dos testes está disponível em:
B3\coveragereport\index.html

## 📁 Geração de Relatório HTML (opcional)

0. Instale o ReportGenerator (caso necessário):

```bash
dotnet tool install --global dotnet-reportgenerator-globaltool
```

1. Execute os testes da solução com coleta de cobertura da pasta raíz do projeto (F:\Projetos_CSharp\B3):

```bash
dotnet test B3.API\B3.API.sln /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=./TestResults/


2. Gere o relatório a partir de todos os arquivos `.cobertura.xml`:

```bash
reportgenerator -reports:"**/TestResults/**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
```

3. Abra o relatório no navegador:

```bash
start .\coveragereport\index.html


################################################################################
# Requisitos Atendidos
################################################################################

[x] Tela Web em Angular CLI com entrada de valor e prazo  
[x] API REST que calcula valor bruto e líquido  
[x] Fórmula de juros composta aplicada mês a mês  
[x] Alíquota regressiva aplicada conforme prazo  
[x] SOLID aplicado na estrutura da aplicação  
[x] Testes automatizados com cobertura acima de 90%  
[x] Projeto containerizado e com instruções no README/ READM.MK  
[x] Zero alertas no SonarLint e Análise de Código do VS  
[x] Repositório público no GitHub

################################################################################
# Autor
################################################################################

Rubens Manhães Bernardes  
https://github.com/rubensmanhaesb
