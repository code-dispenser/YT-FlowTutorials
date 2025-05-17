using Autofac;
using Autofac.Extensions.DependencyInjection;
using Flow.Core.Areas.Extensions;
using FlowTutorials.ConsoleClient.Common.Seeds;
using FlowTutorials.ConsoleClient.Common.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace FlowTutorials.ConsoleClient
{
    internal class Program
    {
        //private static readonly Uri _hostAddress = new("https://localhost:7031");// <<<< Choose your poison .net Kestrel web server.
        private static readonly Uri _hostAddress = new("https://localhost:44380");   // <<<< Choose your poison IISExpress web server.

        static async Task Main()
        {
            
            var container = ConfigureAutofac(_hostAddress);

            using (var scope = container.BeginLifetimeScope())
            {
                var examples     = scope.Resolve<IEnumerable<IFlowExample>>().OrderBy(x => x.Order).ToList();
                var exampleCount = examples.Count;
                var input        = String.Empty;
           
                while (exampleCount > 0)
                {
                    WipeConsoleScreen();

                    GeneralUtils.WriteLine($"{GlobalValues.Console_Full_Screen_Text}\r\n", ConsoleColor.Cyan);

                    examples.ForEach
                    (
                        example => GeneralUtils.WriteLine($"{example.Order}: {example.Name}\r\n" +
                                                         $"Description: {example.Description}\r\n" +
                                                         $"File to breakpoint: {example.FileName}\r\n", ConsoleColor.Gray)
                    );

                    GeneralUtils.WriteLine($"{GlobalValues.Console_Start_Instruction_Text}\r\n", ConsoleColor.Cyan);

                    input = String.IsNullOrWhiteSpace(input) ? GetInput() : input;

                    if (IsExitCharacter(input)) break;

                    var exampleNumber = 0;

                    while (false == (int.TryParse(input, out exampleNumber) && exampleNumber > 0 && exampleNumber <= exampleCount))
                    {
                        if (input.Equals("X", StringComparison.CurrentCultureIgnoreCase)) break;
                        Console.WriteLine($"{GlobalValues.Console_Number_Rule_Text}{exampleCount}");
                        input = GetInput();
                        continue;
                    }

                    if (IsExitCharacter(input)) break;

                    do
                    {
                        WipeConsoleScreen();

                        var codeToPrint = await GeneralUtils.GetCodeSnippet(examples[exampleNumber - 1].FileName, examples[exampleNumber - 1].PrintLineRange);

                        codeToPrint.OnFailure(failure => GeneralUtils.WriteLine(failure.Reason, ConsoleColor.Red)).OnSuccess(success => GeneralUtils.WriteLine(success, ConsoleColor.DarkGray));

                        await examples[exampleNumber -1].RunExample();

                        GeneralUtils.WriteLine($"{GlobalValues.console_Next_Instruction_Text}", ConsoleColor.Cyan);

                        input = GetInput();
                    }
                    while (input.Equals("R", StringComparison.CurrentCultureIgnoreCase));

                    if (true == IsExitCharacter(input)) break;

                    input = String.Empty;
                }
            }
        }

        private static string GetInput()
        
            => Console.ReadLine()?.Trim() ?? String.Empty;

        private static bool IsExitCharacter(string input)
        
            => input.Equals("X", StringComparison.CurrentCultureIgnoreCase);

        private static bool IsContinueCharacter(string input)

            => input.Equals("Y", StringComparison.CurrentCultureIgnoreCase);

        private static void WipeConsoleScreen()
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");//Erase the entire screen buffer, including scrollback (if lines are greater than screen size Console.Clear does not clear everything)
            Console.Clear();
        }

        private static IContainer ConfigureAutofac(Uri baseAddress)
        {
            var builder = new ContainerBuilder();
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<HttpClientDelegatingHandler>()
                                .AddHttpClient(String.Empty, client => client.BaseAddress = baseAddress)
                                    .AddHttpMessageHandler<HttpClientDelegatingHandler>();

            builder.Populate(serviceCollection);

            builder.RegisterAssemblyTypes(typeof(IFlowExample).Assembly).AssignableTo<IFlowExample>().As<IFlowExample>();
                          

            return builder.Build();
        }
    }
}
