using ASPNetBoilerplate.Web;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Environment);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline
startup.Configure(app, builder.Environment);

app.Run();