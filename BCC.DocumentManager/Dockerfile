FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Bcc.DocumentManager/BCC.DocumentManager.csproj", "Bcc.DocumentManager/"]
RUN dotnet restore "Bcc.DocumentManager/BCC.DocumentManager.csproj"
COPY . .
WORKDIR "/src/Bcc.DocumentManager"
RUN dotnet build "BCC.DocumentManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BCC.DocumentManager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BCC.DocumentManager.dll"]