using InfonetAssociates.Infrustructure;
using InfonetAssociates.Services.Extentions;

var builder = WebApplication.CreateBuilder(args);
var conStr = new ConnectionString
{
    Server = builder.Configuration["SqlServer:Server"],
    IntegratedSecurity = builder.Configuration["SqlServer:IntegratedSecurity"],
    Database = builder.Configuration["SqlServer:Database"],
};
Connection.Initialize(conStr);
// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddScopedServices(builder.Configuration);

builder.Services.AddAutoMapper(typeof(AutoMappingProfile).Assembly);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();
