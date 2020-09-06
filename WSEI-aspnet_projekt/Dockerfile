#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_12.x | bash - && \
    apt-get install -y build-essential nodejs
WORKDIR "/src/WSEI-aspnet_projekt/ClientApp"
COPY package*.json ./
RUN npm install
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WSEI-aspnet_projekt/WSEI-aspnet_projekt.csproj", "WSEI-aspnet_projekt/"]
RUN dotnet restore "WSEI-aspnet_projekt/WSEI-aspnet_projekt.csproj"
WORKDIR "/src/WSEI-aspnet_projekt"
RUN dotnet build "WSEI-aspnet_projekt.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WSEI-aspnet_projekt.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WSEI-aspnet_projekt.dll"]
