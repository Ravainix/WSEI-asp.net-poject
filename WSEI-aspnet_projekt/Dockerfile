#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /app

RUN apt-get update
RUN curl -sL https://deb.nodesource.com/setup_12.x | bash -
RUN apt-get install -y nodejs

COPY WSEI-aspnet_projekt/ClientApp/package*.json WSEI-aspnet_projekt/ClientApp/
RUN ["npm","install", "WSEI-aspnet_projekt/ClientApp"]

COPY . ./
RUN ["dotnet", "publish", "WSEI-aspnet_projekt/WSEI-aspnet_projekt.csproj", "-c", "Release", "-o", "build"]


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic
WORKDIR /app
COPY --from=build /app/build .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "WSEI-aspnet_projekt.dll"]
