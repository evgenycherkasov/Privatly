FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Privatly.API/Privatly.API.csproj", "Privatly.API/"]
RUN dotnet restore "Privatly.API/Privatly.API.csproj"
COPY . .
WORKDIR "/src/Privatly.API"
RUN dotnet build "Privatly.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Privatly.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Privatly.API.dll"]
