###############
##Build react##
###############
FROM node:16.15.0 as react-build
RUN mkdir /usr/src/app
WORKDIR /usr/src/app
ENV PATH /usr/src/app/node_modules/.bin:$PATH
COPY ui/. /usr/src/app
RUN npm ci
RUN npm run build

################
##Build dotnet##
################
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dotnet-build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY host/DotnetCore.Template/. ./
RUN dotnet restore

# copy everything else and build app
WORKDIR /app
RUN dotnet publish -c Release -o out --no-restore

##############
##Run dotnet##
##############
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=dotnet-build /app/out ./
COPY --from=react-build /usr/src/app/build ./wwwroot
ENTRYPOINT ["dotnet", "DotnetCore.Template.dll"]