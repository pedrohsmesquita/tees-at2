using Core.Exceptions;
using Core.Repository;
using Core.Service;
using Infrastructure;
using Service;

namespace Rest;

record CriarUsuarioDto(string Nome, string Email);
record CriarPostDto(string Texto);
record ComentarDto(int PostId, string Texto);
record CurtirDto(int PostId);
record SeguirDto(int AlvoId);

public class Program
{
    public static void Main(string[] args)
    {
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
        
        var app = builder.Build();
        
        app.Use(async (ctx, next) =>
        {
            try
            {
                await next();
            }
            catch (NaoEncontradoException ex)
            {
                ctx.Response.StatusCode = StatusCodes.Status404NotFound;
                await ctx.Response.WriteAsJsonAsync(new { erro = ex.Message });
            }
            catch (RegraDeNegocioException ex)
            {
                ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
                await ctx.Response.WriteAsJsonAsync(new { erro = ex.Message });
            }
        });

        app.MapPost("/usuarios", (CriarUsuarioDto dto, IUsuarioService usuarios) =>
        {
            var usuario = usuarios.Criar(dto.Nome, dto.Email);
            return Results.Created($"/usuarios/{usuario.Id}", usuario);
        });
        
        app.MapPost("/usuarios/{id:int}/posts", (int id, CriarPostDto dto, IPostService posts) =>
        {
            var post = posts.Criar(id, dto.Texto);
            return Results.Created($"/posts/{post.Id}", post);
        });
        
        app.MapPost("/usuarios/{id:int}/comentar", (int id, ComentarDto dto, IComentarioService comentarios) =>
        {
            var comentario = comentarios.Comentar(id, dto.PostId, dto.Texto);
            return Results.Ok(comentario);
        });
        
        app.MapPost("/usuarios/{id:int}/curtir", (int id, CurtirDto dto, ICurtidaService curtidas) =>
            Results.Ok(new { criado = curtidas.Curtir(id, dto.PostId) }));
        
        app.MapPost("/usuarios/{id:int}/seguir", (int id, SeguirDto dto, ISegueService segues) =>
            Results.Ok(new { criado = segues.Seguir(id, dto.AlvoId) }));
        
        app.MapGet("/usuarios/{id:int}", (int id, IUsuarioService usuarios) => usuarios.Buscar(id));
        
        app.MapGet("/posts/{id:int}", (int id, IPostService posts) => posts.Buscar(id));

        app.MapGet("/usuarios/{id:int}/posts", (int id, IPostService posts) => posts.PostsDoUsuario(id));

        app.MapGet("/usuarios/{id:int}/feed", (int id, IPostService posts) => posts.Feed(id));

        app.MapGet("/usuarios/{id:int}/seguidores", (int id, ISegueService segues) => segues.SeguidoresDe(id));

        app.MapGet("/usuarios/{id:int}/seguindo", (int id, ISegueService segues) => segues.SeguindoDe(id));
        
        app.Run();
    }
}