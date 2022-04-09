using Application.Logic;
using Contracts.Services;
using Entities.Interfaces;
using FileData.JsonDataAccess;
using JsonDataAccess.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IPostService, PostServiceImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<ICommentService, CommentServiceImpl>();

builder.Services.AddScoped<JsonContext>();
builder.Services.AddScoped<IPostDao, PostJsonDao>();
builder.Services.AddScoped<IUserDao, UserJsonDao>();
builder.Services.AddScoped<ICommentDao, CommentJsonDao>();

var app = builder.Build();

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