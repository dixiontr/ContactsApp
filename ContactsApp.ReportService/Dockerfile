FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY "packages/ContactsApp.Core.1.0.1.nupkg" "packages/"
RUN dotnet nuget add source /src/packages

COPY ["ContactsApp.ReportService/ContactsApp.ReportService.csproj", "ContactsApp.ReportService/"]
RUN dotnet restore "ContactsApp.ReportService/ContactsApp.ReportService.csproj"
COPY . .
WORKDIR "/src/ContactsApp.ReportService"
RUN dotnet build "ContactsApp.ReportService.csproj" -c Release -o /app/build/ContactsApp.ReportService/


FROM build AS publish
RUN dotnet publish "ContactsApp.ReportService.csproj" -c Release -o /app/publish/ContactsApp.ReportService/

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir ContactsApp.ReportService/Reports
WORKDIR "/app/ContactsApp.ReportService"
ENTRYPOINT ["dotnet", "ContactsApp.ReportService.dll"]

