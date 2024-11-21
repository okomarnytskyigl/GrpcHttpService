using GrpcHttpService.Grpc.Protos;
using GrpcHttpService.Http.Endpoints;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(5000);
//});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpcClient<OrganizationService.OrganizationServiceClient>(o =>
{
    o.Address = new Uri(Environment.GetEnvironmentVariable("GRPC_SERVICE_URL") ?? "http://localhost:5001");
});

builder.Services.AddGrpcClient<UserService.UserServiceClient>(o =>
{
    o.Address = new Uri(Environment.GetEnvironmentVariable("GRPC_SERVICE_URL") ?? "http://localhost:5001");
});

var app = builder.Build();

app.MapOrganizationGrpcEndpoints();
app.MapUserGrpcEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
