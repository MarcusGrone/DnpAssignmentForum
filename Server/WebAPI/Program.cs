using RepositoryContracts;
using EfcRepositories.Repositories;
using Microsoft.EntityFrameworkCore;
using AppContext = EfcRepositories.AppContext;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppContext>(options =>
    options.UseSqlite("Data Source=C:\\Users\\Marcus Lykkegaard\\Desktop\\Softwareteknologi\\Softwareprogrammer\\RiderProjects\\DnpAssignmentForum\\Server\\EfcRepositories\\app.db"));


builder.Services.AddScoped<IPostRepository, EfcPostRepository>();
builder.Services.AddScoped<IUserRepository, EfcUserRepository>();
builder.Services.AddScoped<ICommentRepository, EfcCommentRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();