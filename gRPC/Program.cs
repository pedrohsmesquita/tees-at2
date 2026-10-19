using Core.Repository;
using Core.Service;
using Infrastructure;
using Service;
using TEES_AT2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddSingleton<IPostRepository, PostRepository>();
builder.Services.AddSingleton<IComentarioRepository, ComentarioRepository>();
builder.Services.AddSingleton<ICurtidaRepository, CurtidaRepository>();
builder.Services.AddSingleton<ISegueRepository, SegueRepository>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();
builder.Services.AddScoped<ICurtidaService, CurtidaService>();
builder.Services.AddScoped<ISegueService, SegueService>();

builder.Services.AddGrpc(options => options.Interceptors.Add<ErrosInterceptor>());
builder.Services.AddGrpcReflection();

var app = builder.Build();

app.MapGrpcService<RedeSocialGrpcService>();

if (app.Environment.IsDevelopment())
    app.MapGrpcReflectionService();

app.Run();