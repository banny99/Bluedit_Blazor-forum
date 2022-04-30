using Application.Interfaces;
using Application.Logic;
using Contracts.Services;
using EfcData.EfcDataAccess;
using Entities.Interfaces;
using FileData.JsonDataAccess;
using JsonDataAccess.Context;

using (ForumDbContext ctx = new())
{
    ctx.Seed();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IPostService, PostServiceImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<ICommentService, CommentServiceImpl>();

//using-file 
// builder.Services.AddScoped<JsonContext>();
// builder.Services.AddScoped<IPostDao, PostJsonDao>();
// builder.Services.AddScoped<IUserDao, UserJsonDao>();
// builder.Services.AddScoped<ICommentDao, CommentJsonDao>();
//
//using-db
builder.Services.AddDbContext<ForumDbContext>();
builder.Services.AddScoped<IPostDao, PostSqliteDao>();
builder.Services.AddScoped<IUserDao, UserSqliteDao>();
builder.Services.AddScoped<ICommentDao, CommentSqliteDao>();


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