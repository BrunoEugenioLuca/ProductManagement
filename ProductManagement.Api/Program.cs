
using ProductManagement.Bl;
using ProductManagement.Dal.Data;
using ProductManagement.Producer;
using System.Text.Json.Serialization;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7280")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .WithExposedHeaders("*");
                      });
});



// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configure DI

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IProducerMessage, ProducerMessage>();
builder.Services.AddDbContext<ProductManagementContext>();

// end DI

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

// allow origin

app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

// end

app.UseAuthorization();

app.MapControllers();

app.Run();
