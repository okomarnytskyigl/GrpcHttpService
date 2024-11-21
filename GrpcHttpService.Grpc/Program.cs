using GrpcHttpService.DataAccess;
using GrpcHttpService.BusinessLogic;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(8080);
//});

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.RegisterDataAccessDependencies(builder.Configuration);
builder.Services.RegisterBusinessLogicDependencies();

var app = builder.Build();

app.Run();
