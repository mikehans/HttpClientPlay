using HttpClientPlay;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHttpClient();
    })
    .UseSerilog()
    .Build();

Log.Information("Here we go now!");

IHttpClientFactory httpClientFactory = host.Services.GetRequiredService<IHttpClientFactory>();

GenericDataLoader<PostModel> postDataLoader = new GenericDataLoader<PostModel>(httpClientFactory);

var post = await postDataLoader.LoadData("posts", 1);

Console.WriteLine($"ID: {post.Id} \tTitle: {post.Title}");

PostsProcessor postsProcessor = new PostsProcessor(httpClientFactory);
PostModel postModel = await postsProcessor.LoadPost(1);
Console.WriteLine($"ID: {postModel.Id} \tTitle: {postModel.Title}");


GenericDataLoader<UserModel> userDataLoader = new GenericDataLoader<UserModel>(httpClientFactory);
var users = await userDataLoader.LoadData("users", 1);
Console.WriteLine($"ID: {users.Id}\tName: {users.Name}");

await host.RunAsync();