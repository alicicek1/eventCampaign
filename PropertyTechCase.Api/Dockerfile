FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PropertyTechCase.Api/PropertyTechCase.Api.csproj", "PropertyTechCase.Api/"]
COPY ["PropertyTechCase.Database/PropertyTechCase.Database.csproj", "PropertyTechCase.Database/"]
RUN dotnet restore "PropertyTechCase.Api/PropertyTechCase.Api.csproj"
COPY . .
WORKDIR "/src/PropertyTechCase.Api"
RUN dotnet build "PropertyTechCase.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PropertyTechCase.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PropertyTechCase.Api.dll"]
