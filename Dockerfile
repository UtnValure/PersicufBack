# Usa la imagen de .NET 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Persicuf/Persicuf.csproj", "Persicuf/"]
RUN dotnet restore "Persicuf/Persicuf.csproj"
COPY . .
WORKDIR "/src/Persicuf"
RUN dotnet build "Persicuf.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Persicuf.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Persicuf.dll"]
