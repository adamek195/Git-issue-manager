# Git-issue-manager
A microservice for managing Git Issues across multiple Git hosting providers.

Built with:
- .NET 8.0
- Docker
- Docker Compose

## How to run

1. In the main directory, create **.env** file and define environment variables:

1.1 GitHub

- GITHUB_TOKEN - generate a Personal Access Token in GitHub
- GITHUB_URL - https://api.github.com/
- GITHUB_USER_AGENT - set user agent
- GITHUB_ENABLED - set flag true or false

1.2 GitLab

GITLAB_TOKEN- generate a Personal Access Token in GitLab
GITLAB_URL - https://gitlab.com/api/v4/
GITLAB_USER_AGENT - set user agent
GITLAB_ENABLED - set flag true or false

2. Create file **appsettings.json** with the same settings as in **appsettings.defualt.json** in GitIssueManager.Api folder.

3. In in appsetting.json, configure with the same environment variables defined in the **.env** file.

4. In the main directory run the commands:

```bash
docker-compose build
docker-compose up
```

5. To stop the server press Ctrl + C, then run the command:

```bash
docker-compose down
```

## How to run tests

1. In folder scripts run the command:
```bash
dotnet-test.ps1
```
