FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY host/DotnetCore.Template/. ./
RUN dotnet restore

# copy everything else and build app
WORKDIR /app
RUN dotnet publish host/DotnetCore.Template/DotnetCore.Template.csproj -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "DotnetCore.Template.dll"]