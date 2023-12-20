FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Lab5/CurrencyService.WebTest/CurrencyService.WebTest.csproj", "src/Lab5/CurrencyService.WebTest/"]
RUN dotnet restore "src/Lab5/CurrencyService.WebTest/CurrencyService.WebTest.csproj"
COPY . .
WORKDIR "/src/src/Lab5/CurrencyService.WebTest"
RUN dotnet build "CurrencyService.WebTest.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CurrencyService.WebTest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CurrencyService.WebTest.dll"]
