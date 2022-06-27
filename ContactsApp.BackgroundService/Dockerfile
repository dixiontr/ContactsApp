FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY "packages/ContactsApp.Core.1.0.1.nupkg" "packages/"
RUN dotnet nuget add source /src/packages

COPY ["ContactsApp.BackgroundService/ContactsApp.BackgroundService.csproj", "ContactsApp.BackgroundService/"]
RUN dotnet restore "ContactsApp.BackgroundService/ContactsApp.BackgroundService.csproj"
COPY . .
WORKDIR "/src/ContactsApp.BackgroundService"
RUN dotnet build "ContactsApp.BackgroundService.csproj" -c Release -o /app/build/ContactsApp.BackgroundService/


FROM build AS publish
RUN dotnet publish "ContactsApp.BackgroundService.csproj" -c Release -o /app/publish/ContactsApp.BackgroundService/

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir ContactsApp.ReportService
RUN mkdir ContactsApp.ReportService/Reports
WORKDIR "/app/ContactsApp.BackgroundService"
ENTRYPOINT ["dotnet", "ContactsApp.BackgroundService.dll"]
