using Flow.Core.Areas.Returns;
using Flow.Core.Areas.Utilities;
using FlowTutorials.ConsoleClient.Common.Seeds;
using When.Core.Extensions;

namespace FlowTutorials.ConsoleClient.Common.Utilities;

public static class GeneralUtils
{
    public static async Task<Flow<string>> GetCodeSnippet(string fileName, Range printLineRange)
    {
        var filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Examples", fileName));

        return await FlowHandler.TryToFlow(async () =>
        {
            var lines = await File.ReadAllLinesAsync(filePath);

            string[] snippetLines = printLineRange.End.Value < lines.Length ? lines[printLineRange] : lines[printLineRange.Start .. ];

            return String.Join(Environment.NewLine, snippetLines) + Environment.NewLine;
        },
        _ => Flow<string>.Failed(new Failure.FileSystemFailure($"{GlobalValues.Unable_To_Locate_Or_Read_File_Text} {filePath}")));
    }

    public static void WriteLine(string textToWrite, ConsoleColor? foregroundColour = null)
    {
        var currentColour = Console.ForegroundColor;

        foregroundColour.HasValue.WhenTrue(() => Console.ForegroundColor = foregroundColour!.Value);
        
        Console.WriteLine(textToWrite);

        Console.ForegroundColor = currentColour;

    }

    public static async Task<string> ReadJsonRuleFile(string filePath)

        => await File.ReadAllTextAsync(filePath);
}



