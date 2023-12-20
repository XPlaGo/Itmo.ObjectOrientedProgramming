FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Lab5/IdentityService/IdentityService/IdentityService.csproj", "src/Lab5/IdentityService/IdentityService/"]
RUN dotnet restore "src/Lab5/IdentityService/IdentityService/IdentityService.csproj"
COPY . .
WORKDIR "/src/src/Lab5/IdentityService/IdentityService"
RUN dotnet build "IdentityService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.dll"]
