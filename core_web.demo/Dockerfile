FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["core_web.demo.csproj", "core_web.demo/"]
RUN dotnet restore "core_web.demo/core_web.demo.csproj"
WORKDIR "/src/AspDotNetCoreWeb"
COPY . .
RUN dotnet build "core_web.demo.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "core_web.demo.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "core_web.demo.dll"]
