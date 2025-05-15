using Microsoft.AspNetCore.Mvc;
using Moq;
using GitIssueManager.Api.Controllers;
using GitIssueManager.Core;
using GitIssueManager.Core.Dtos;
using GitIssueManager.Core.Strategies;

namespace GitIssueManage.Tests
{
    public class IssuesControllerTests
    {
        private IssuesController CreateController(params IServiceStrategy[] strategies)
        {
            return new IssuesController(strategies);
        }

        [Fact]
        public async Task Create_ReturnsCreated_WhenStrategyAvailable()
        {
            var dto = new IssueCommandDto { Owner = "o", Repo = "r", Title = "T", Body = "B" };
            var createdDto = new IssueDto { IssueNumber = 7, Title = "T", Body = "B" };

            var mock = new Mock<IServiceStrategy>();
            mock
                .Setup(s => s.Supports(GitHostingServiceType.GitHub))
                .Returns(true);
            mock
                .Setup(s => s.CreateAsync(dto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdDto);

            var controller = CreateController(mock.Object);

            var result = await controller.Create("GitHub", dto, CancellationToken.None);

            var created = Assert.IsType<CreatedResult>(result);
            Assert.Equal(201, created.StatusCode);
            Assert.Equal(createdDto, created.Value);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenServiceUnsupported()
        {
            var controller = CreateController();
            var dto = new IssueCommandDto();

            var result = await controller.Create("unknown", dto, CancellationToken.None);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Unsupported service", bad.Value.ToString());
        }

        [Fact]
        public async Task Update_ReturnsOk_WhenStrategyAvailable()
        {
            var dto = new IssueCommandDto { Title = "T", Body = "B" };
            var updatedDto = new IssueDto { IssueNumber = 5, Title = "T", Body = "B" };

            var mock = new Mock<IServiceStrategy>();
            mock.Setup(s => s.Supports(GitHostingServiceType.GitLab)).Returns(true);
            mock
                .Setup(s => s.UpdateAsync(5, dto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(updatedDto);

            var ctrl = CreateController(mock.Object);

            var result = await ctrl.Update("GitLab", 5, dto, CancellationToken.None);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(updatedDto, ok.Value);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenServiceUnsupported()
        {
            var controller = CreateController();
            var dto = new IssueCommandDto();

            var result = await controller.Update("badservice", 1, dto, CancellationToken.None);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Unsupported service", bad.Value.ToString());
        }

        [Fact]
        public async Task Close_ReturnsNoContent_WhenStrategyAvailable()
        {
            var mock = new Mock<IServiceStrategy>();
            mock.Setup(s => s.Supports(GitHostingServiceType.GitHub)).Returns(true);
            mock
                .Setup(s => s.CloseAsync(3, It.IsAny<StateIssueDto>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var controller = CreateController(mock.Object);
            var result = await controller.Close("GitHub", 3, new StateIssueDto(), CancellationToken.None);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Close_ReturnsBadRequest_WhenServiceUnsupported()
        {
            var controller = CreateController();
            var result = await controller.Close("badservice", 1, new StateIssueDto(), CancellationToken.None);
            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Unsupported service", bad.Value.ToString());
        }
    }
}