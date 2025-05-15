$gitIssueManagerApiDir = $PSScriptRoot.Substring(0, $PSScriptRoot.LastIndexOf("\"))

dotnet test $gitIssueManagerApiDir\GitIssueManager\GitIssueManager\GitIssueManager.Tests\GitIssueManager.Tests.csproj