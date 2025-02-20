using Application.Interfaces;
using Application.Mappings;
using Infraestructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(BookProfile));
builder.Services.AddAutoMapper(typeof(AuthorProfile));

builder.Services.AddHttpClient<IBookService, BookService>();
builder.Services.AddHttpClient<IAuthorService, AuthorService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp", policy => policy.WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AngularApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
