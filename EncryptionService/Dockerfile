#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["EncryptionService/EncryptionService.csproj", "EncryptionService/"]
RUN dotnet restore "EncryptionService/EncryptionService.csproj"
COPY . .
WORKDIR "/src/EncryptionService"
RUN dotnet build "EncryptionService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EncryptionService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EncryptionService.dll"]

#docker run -d  -v "C:\Users\richa\AppData\Roaming\Microsoft\UserSecrets:/root/.microsoft/usersecrets:ro" -v "C:\Users\richa\AppData\Roaming\ASP.NET\Https:/root/.aspnet/https:ro" -e "ASPNETCORE_ENVIRONMENT=Development" -e "ASPNETCORE_URLS=https://+:443;http://+:80"  -p 32770:443 -p 32771:80 --name rhz-encrypt encryptionservice:latest
#docker run -d -p 32770:443 -p 32771:80 --name rhz-encrypt encryptionservice:latest