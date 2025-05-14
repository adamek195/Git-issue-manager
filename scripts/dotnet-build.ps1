$gitIssueManagerApiDir = $PSScriptRoot.Substring(0, $PSScriptRoot.LastIndexOf("\"))

dotnet build $gitIssueManagerApiDir\GitIssueManager\GitIssueManager\GitIssueManager.Api\GitIssueManager.Api.csproj