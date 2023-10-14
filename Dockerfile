FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["FrontEnd_GAMA/FrontEnd_GAMA.csproj", "."]
RUN dotnet restore "FrontEnd_GAMA.csproj"

COPY . .
RUN dotnet build "FrontEnd_GAMA.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FrontEnd_GAMA.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FrontEnd_GAMA.dll"]



