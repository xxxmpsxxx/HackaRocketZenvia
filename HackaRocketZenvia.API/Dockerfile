#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["HackaRocketZenvia.API.csproj", "HackaRocketZenvia.API/"]
RUN dotnet restore "HackaRocketZenvia.API/HackaRocketZenvia.API.csproj"
WORKDIR "/src/HackaRocketZenvia.API"
COPY . .
RUN dotnet build "HackaRocketZenvia.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HackaRocketZenvia.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "HackaRocketZenvia.API.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet HackaRocketZenvia.API.dll