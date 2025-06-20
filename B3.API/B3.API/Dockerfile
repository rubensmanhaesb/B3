# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar arquivos da solution
COPY B3.API/B3.API.sln ./
COPY B3.API/B3.API/B3.API.csproj ./B3.API/
COPY B3.Application/B3.Application.csproj ./B3.Application/
COPY B3.Domain/B3.Domain.csproj ./B3.Domain/

# Restaurar pacotes
RUN dotnet restore B3.API/B3.API.csproj

# Copiar o restante dos arquivos
COPY . .

# Build e publish
WORKDIR /src/B3.API/B3.API
RUN dotnet publish B3.API.csproj -c Release -o /app/publish

# Etapa 2: Imagem final
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "B3.API.dll"]
