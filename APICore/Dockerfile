FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["APICore.API/APICore.API.csproj", "APICore.API/"]
COPY ["APICore.Common/APICore.Common.csproj", "APICore.Common/"]
COPY ["APICore.Data/APICore.Data.csproj", "APICore.Data/"]
COPY ["APICore.Services/APICore.Services.csproj", "APICore.Services/"]
RUN dotnet restore "APICore.API/APICore.API.csproj"
COPY . .
WORKDIR "/src/APICore.API"
RUN dotnet build "APICore.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APICore.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet APICore.API.dll