using GitIssueManager.Core.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceStrategies(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
