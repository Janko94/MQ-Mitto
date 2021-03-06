#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CAP/CAP.csproj", "CAP/"]
COPY ["Service/Service.csproj", "Service/"]
COPY ["Interface/Interface.csproj", "Interface/"]
COPY ["Model/Model.csproj", "Model/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "CAP/CAP.csproj"
COPY . .
WORKDIR "/src/CAP"
RUN dotnet build "CAP.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CAP.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CAP.dll"]
