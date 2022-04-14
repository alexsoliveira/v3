#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Application/TGS.Cartorio.Application.Api/TGS.Cartorio.Application.Api.csproj", "Application/TGS.Cartorio.Application.Api/"]
COPY ["Infrastructure/TGS.Cartorio.Infrastructure.IoC/TGS.Cartorio.Infrastructure.IoC.csproj", "Infrastructure/TGS.Cartorio.Infrastructure.IoC/"]
COPY ["Infrastructure/TGS.Cartorio.Infrastructure.Redis/TGS.Cartorio.Infrastructure.Redis.csproj", "Infrastructure/TGS.Cartorio.Infrastructure.Redis/"]
COPY ["Domain/TGS.Cartorio.Domain/TGS.Cartorio.Domain.csproj", "Domain/TGS.Cartorio.Domain/"]
COPY ["Infrastructure/TGS.Cartorio.Infrastructure.SqlServer/TGS.Cartorio.Infrastructure.SqlServer.csproj", "Infrastructure/TGS.Cartorio.Infrastructure.SqlServer/"]
COPY ["Application/TGS.Cartorio.Application/TGS.Cartorio.Application.csproj", "Application/TGS.Cartorio.Application/"]
COPY ["Infrastructure/TGS.Cartorio.Utility/TGS.Cartorio.Infrastructure.Utility.csproj", "Infrastructure/TGS.Cartorio.Utility/"]
COPY ["Infrastructure/TGS.Cartorio.Infrastructure.Configs/TGS.Cartorio.Infrastructure.Configs.csproj", "Infrastructure/TGS.Cartorio.Infrastructure.Configs/"]
RUN dotnet restore "Application/TGS.Cartorio.Application.Api/TGS.Cartorio.Application.Api.csproj"
COPY . .
WORKDIR "/src/Application/TGS.Cartorio.Application.Api"
RUN dotnet build "TGS.Cartorio.Application.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TGS.Cartorio.Application.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TGS.Cartorio.Application.Api.dll"]