using ItExpertTestApi.DAL.DbConnectionProviders;
using ItExpertTestApi.Items;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new ItemInJsonConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("Postgres")
    ?? throw new Exception("Provide connection string");
builder.Services.AddNpgsqlDbConnectionProvider(connectionString);

builder.Services.AddScoped<IItemService, ItemService>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
