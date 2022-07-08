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

- AppSetting UiUrl (http://localhost:3000 in dev) used to set CORS headers.

## The SPA (react version)

https://create-react-app.dev/

Created with:

>> npx create-react-app ui-template --template typescript

Start with:

>> npm run start

Changes to work with the api:

- Environment files with REACT_APP_API_BASE_URL pointing to the api base url (https://localhost:7222 in dev).
- ApiClient.ts which just exposes fetchApi that is the same as fetch except you instead of using say fetch('https://localhost:7222/api/some-items') you use fetchApi('api/some-items') and the base url is added.

