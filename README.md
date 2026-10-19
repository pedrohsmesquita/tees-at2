## GRPC

O projeto .NET `gRPC` deve ser executado para realizar os testes utilizando o gRPC. Basta abrir a solution no Visual Studio, executar o projeto e obter a porta aberta localmente.

### Realizando testes

Na ferramenta para realizar a requisição, como o Bruno, por exemplo, deve-se inserir o arquivo ./Protos/rede.proto para utilizar os métodos do gRPC por meio de HTTP.

#### Métodos e corpo da requisição gRPC

1. CriarUsuario
{ "nome": "Ana", "email": "ana@email.com" }
{ "nome": "Bruno", "email": "bruno@email.com" }

2. Criar Post
{ "usuario_id": 2, "texto": "Meu primeiro post" }

3. Seguir
{ "usuario_id": 1, "alvo_id": 2 }

4. Curtir
{ "usuario_id": 1, "alvo_id": 1 }

5. Comentar
{ "usuario_id": 1, "post_id": 1, "texto": "Muito bom!" }

6. BuscarUsuario
{ "id": 1 }

7. BuscarPost
{ "id": 1 }

8.PostsDoUsuario
{ "id": 2 }

9. FeedDoUsuario
{ "id": 1 }

10. SeguidoresDe
{ "id": 2 }

11. SeguindoDe
{ "id": 1 }

## HTML

O projeto .NET `Rest` possui a implementado endpoints dos comandos e consultas do gRPC, mas utilizando o padrão Rest, apesar de não ter sido impĺementado as HATEOAS. Basta abrir a solution no Visual Studio, executar o projeto e obter a porta aberta localmente.

### Realizando testes

Assim como o gRPC, foi utilizado a ferramenta Bruno para testar todos os comandos e consultas por meio de HTTP.

#### Requisiçoes HTTP

1. POST /usuarios
{ "nome": "Ana", "email": "ana@email.com" }
{ "nome": "Bruno", "email": "bruno@email.com" }

2. GET usuarios/1

3.POST /usuarios/2/posts
{ "texto": "Meu primeiro post" }

4. POST /usuarios/1/seguir
{ "alvoId": 2 }

5. POST /usuarios/1/curtir
{ "postId": 1 }

6. POST /usuarios/1/comentar
{ "postId": 1, "texto": "Muito bom!" }

7. GET /usuarios/1/feed

8. GET /usuarios/2/seguidores

9. GET /usuarios/1/seguindo

10. GET /usuarios/2/posts

11. GET /posts/1
