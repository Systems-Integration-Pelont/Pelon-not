FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["apiUsuarioCuenta/apiUsuarioCuenta.csproj", "apiUsuarioCuenta/"]
RUN dotnet restore "apiUsuarioCuenta/apiUsuarioCuenta.csproj"
COPY . .
WORKDIR "/src/apiUsuarioCuenta"
RUN dotnet build "apiUsuarioCuenta.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "apiUsuarioCuenta.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "apiUsuarioCuenta.dll"]