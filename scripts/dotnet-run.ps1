$gitIssueManagerApiDir = $PSScriptRoot.Substring(0, $PSScriptRoot.LastIndexOf("\"))

dotnet run --project $gitIssueManagerApiDir\GitIssueManager\GitIssueManager\GitIssueManager.Api\GitIssueManager.Api.csproj