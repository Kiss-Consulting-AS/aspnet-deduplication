FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Kiss.AspNet.Deduplication.TestApplication/Kiss.AspNet.Deduplication.TestApplication.csproj", "Kiss.AspNet.Deduplication.TestApplication/"]
RUN dotnet restore "Kiss.AspNet.Deduplication.TestApplication/Kiss.AspNet.Deduplication.TestApplication.csproj"
COPY . .
WORKDIR "/src/Kiss.AspNet.Deduplication.TestApplication"
RUN dotnet build "Kiss.AspNet.Deduplication.TestApplication.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Kiss.AspNet.Deduplication.TestApplication.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kiss.AspNet.Deduplication.TestApplication.dll"]
