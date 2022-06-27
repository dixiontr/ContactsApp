FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY "packages/ContactsApp.Core.1.0.1.nupkg" "packages/"
RUN dotnet nuget add source /src/packages

COPY ["ContactsApp.ContactService/ContactsApp.ContactService.csproj", "ContactsApp.ContactService/"]
RUN dotnet restore "ContactsApp.ContactService/ContactsApp.ContactService.csproj"
COPY . .
WORKDIR "/src/ContactsApp.ContactService"
RUN dotnet build "ContactsApp.ContactService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactsApp.ContactService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactsApp.ContactService.dll"]
