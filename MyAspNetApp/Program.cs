var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/hello", () => "Hello from ASP.NET Core 8 -  Update Commit!");

app.Run();


public partial class Program { }