# Template

This is a template for creating a dotnetcore project with
- a SPA ui (react in this example) under /
- a dotnet core api backend under /api
- both hosted in the dotnet core app
- in development the spa project is run separately and api calls are made to the ui with CORS headers allowing
  cross domain calls so no fancy proxying crap that interfers with hot reload and similar SPA features
- in production the SPA dist is just served as static content by the dotnetcore host
- The same setup should work with angular or any other spa framework that can produce a folder of static output
  for production.
- Docker image for an example production deployment included

## DotnetCore.Template.csproj

Created with
>> dotnet new webapi -minimal

## The SPA

