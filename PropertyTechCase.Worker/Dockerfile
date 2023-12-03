FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PropertyTechCase.Worker/PropertyTechCase.Worker.csproj", "PropertyTechCase.Worker/"]
COPY ["PropertyTechCase.Database/PropertyTechCase.Database.csproj", "PropertyTechCase.Database/"]
RUN dotnet restore "PropertyTechCase.Worker/PropertyTechCase.Worker.csproj"
COPY . .
WORKDIR "/src/PropertyTechCase.Worker"
RUN dotnet build "PropertyTechCase.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PropertyTechCase.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["application.json", "."] 
ENTRYPOINT ["dotnet", "PropertyTechCase.Worker.dll"]
