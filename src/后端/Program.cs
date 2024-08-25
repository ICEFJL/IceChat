using icechat.api.Data;
using Microsoft.EntityFrameworkCore;
using icechat.api.Service;
using icechat.api.Service.Impl;
using icechat.api.Mapper;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(120);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//в╒╡Аиообнд
builder.Services.AddHttpContextAccessor();
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGroupsService, GroupsService>();
builder.Services.AddScoped<IFriendsService, FriendsService>();

builder.Services.AddScoped<IUserGroupsRepository, UserGroupsRepository>();
builder.Services.AddScoped<IGroupsRepository, GroupsRepository>();
builder.Services.AddScoped<IPrivateMessagesRepository, PrivateMessagesRepository>();
builder.Services.AddScoped<IGroupMessagesRepository, GroupMessagesRepository>();
builder.Services.AddScoped<IFriendsRepository, FriendsRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    //SeedData.Reset(services);
    SeedData.Initialize(services);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSession();

app.UseAuthorization();

app.MapControllers();

app.Run();
