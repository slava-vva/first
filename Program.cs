using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Dumpify;
using System.Data.Common;

var serviceCollection = new ServiceCollection();

serviceCollection.AddTransient<IMyServiceClient, MyServiceClient>();
serviceCollection.AddTransient<MyServices>();

var provider = serviceCollection.BuildServiceProvider();

MyServices myService = provider.GetRequiredService<MyServices>();

var stars = myService.GetStars("Hello World!!!").Dump("something");

Console.Write($"Count if Stars = {stars}");



class MyServices
{
    public MyServices(IMyServiceClient _myclient){
            _myClient = _myclient;
    }

    public IMyServiceClient _myClient;

    //public MyServices() => _myClient = new MyServiceClient();

    public int GetStars(string reponame)
    {
        return _myClient.GetRepo(reponame).Stars;
    }
}

internal interface IMyServiceClient
{
    (string repoName, int Stars) GetRepo(string repoName);
}

internal class MyServiceClient : IMyServiceClient
{
    public MyServiceClient()
    {
    }

    public (string repoName, int Stars) GetRepo(string repoName)
    {
        return (repoName, repoName.Length);
    }
}