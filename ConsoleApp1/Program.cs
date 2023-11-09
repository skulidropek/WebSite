using ConsoleApp1;
using Microsoft.Extensions.DependencyInjection;
using RoslynLibrary.Extensions;
using RoslynLibrary.Sections;

var serviceCollection = new ServiceCollection();
serviceCollection.AddPluginAnalyzer();
serviceCollection.AddSingleton<TestService>();
serviceCollection.Configure<ManagedSection>((manage) =>
{
    manage.Path = "C:\\Server Rust\\RustDedicated_Data\\Managed";
});

var serviceProvider = serviceCollection.BuildServiceProvider();

var testService = serviceProvider.GetRequiredService<TestService>();
testService.Start();

Console.ReadLine();