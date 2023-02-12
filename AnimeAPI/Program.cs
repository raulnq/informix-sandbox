using AnimeAPI.Controllers;
using Dapper;
using IBM.Data.Db2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = "Database=sysmaster;Server=localhost:9089;UID=informix;PWD=in4mix;";
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(new Repository(connectionString));
var app = builder.Build();

using (var conn = new DB2Connection(connectionString))
{
    var exists = false;

    conn.Open();

    var reader = await conn.ExecuteReaderAsync("select 1 from systables where tabname = 'animes2'");

    if (reader.HasRows)
    {
        exists = true;
    }

    if (!exists)
    {
        var sql = @"CREATE TABLE animes2
        (
        id varchar(36) PRIMARY KEY,
        name varchar(255) NOT NULL
        );";

        await conn.ExecuteAsync(sql);

    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();