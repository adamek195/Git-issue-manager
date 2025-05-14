FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
EXPOSE 5000

COPY GitIssueManager.sln ./
COPY GitIssueManager.Api/GitIssueManager.Api.csproj GitIssueManager.Api/
COPY GitIssueManager.Core/GitIssueManager.Core.csproj GitIssueManager.Core/

RUN dotnet restore

COPY . ./
RUN dotnet publish GitIssueManager.Api/GitIssueManager.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "GitIssueManager.Api.dll"]