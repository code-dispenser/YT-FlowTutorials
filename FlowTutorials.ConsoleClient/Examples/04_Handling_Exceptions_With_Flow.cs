using Flow.Core.Areas.Extensions;
using Flow.Core.Areas.Returns;
using Flow.Core.Areas.Utilities;
using Flow.Core.Common.Models;
using FlowTutorials.ConsoleClient.Common.Models;
using FlowTutorials.ConsoleClient.Common.Seeds;
using FlowTutorials.ConsoleClient.Common.Utilities;
using System.Text.Json;

namespace FlowTutorials.ConsoleClient.Examples
{
    internal class Handling_Exceptions_With_Flow : IFlowExample
    {
        public Range PrintLineRange => 24..86;
        public string FileName => "04_Handling_Exceptions_With_Flow.cs";
        public int Order => 4;
        public string Name => "Handling exceptions";
        public string Description => "Shows how to use handle exceptions when using Flow";

        public async Task RunExample()

            => await Handling_Exceptions_With_Try();

        public static async Task Handling_Exceptions_With_Try()
        {
            /*
                * You can use the extensions methods OnSuccessTry and OnFailureTry or FlowHandler utility TryToFlow methods when the method you are calling may throw an exception
                * that you want to catch an put in a flow, these will ideally only be on the edges of you app and should be few and far between.
            */
            var errorCausingPath = (await CauseRandomFailure()) ? Path.Combine(Directory.GetCurrentDirectory(), GlobalValues.Json_Registration_Bad_File_SubPath) : "Bad Path";

            var resultToFlow = await FlowHandler.TryToFlow(() => ProcessJsonFile(errorCausingPath), ex => JsonFileExceptionHandler<Registration>(ex))
                                                     .OnFailure(failure => GeneralUtils.WriteLine($"1st output for resultToFlow: {failure.Reason}", ConsoleColor.Red))
                                                        .OnFailureTry(failure => ProcessJsonFile(Path.Combine(Directory.GetCurrentDirectory(), GlobalValues.Json_Registration_File_SubPath)), ex => JsonFileExceptionHandler<Registration>(ex))
                                                            .OnFailure(failure => GeneralUtils.WriteLine($"2nd output for resultToFlow: {failure.Reason}", ConsoleColor.Red))
                                                                .OnSuccess(success => GeneralUtils.WriteLine($"2nd Output for resultToFlow: {success}\r\n", ConsoleColor.Green));
     

            var alreadyAFlow = await ProcessJsonFile(Path.Combine(Directory.GetCurrentDirectory(), GlobalValues.Json_Registration_File_SubPath))
                                        .OnSuccess(success => GeneralUtils.WriteLine($"File processed, trying to send email to {success.EmailAddress} . . ."))
                                            .OnSuccessTry(SendEmail, ex => new Failure.MessagingFailure(ex.Message))
                                                .OnSuccess(success => GeneralUtils.WriteLine($"Email sent successfully:",ConsoleColor.Green))
                                                    .OnFailure(failure => GeneralUtils.WriteLine($"{failure.Reason}\r\n", ConsoleColor.Red));
        }
        private static async Task<Flow<None>> SendEmail(Registration registration)
        {
            if (await CauseRandomFailure()) return Flow<None>.Success(None.Value);

            return new Failure.MessagingFailure($"Authentication failed for the account: {registration.EmailAddress}");
        }
        private static async Task<Flow<Registration>> ProcessJsonFile(string filePath)
        {
            using (var fileStream = File.OpenRead(filePath))
            {
                return (await JsonSerializer.DeserializeAsync<Registration>(fileStream))!;
            }
        }
        private static Flow<T> JsonFileExceptionHandler<T>(Exception exceptionToHandle)//could be a Handle method in an IJsonExceptionHandler Interface for dependency injection.

            => exceptionToHandle switch
            {
                FileNotFoundException fileEx         => new Failure.FileSystemFailure(fileEx.Message),
                JsonException         jsonEx         => new Failure.JsonFailure(jsonEx.Message),
                NotSupportedException notSupportedEx => new Failure.JsonFailure(notSupportedEx.Message), 

                _ => new Failure.UnknownFailure($"Unknown Exception: {exceptionToHandle.Message}")
            };

        public static async Task<bool> CauseRandomFailure()
        {
            await Task.Delay(1);
            return Random.Shared.Next(1, 3) % 2 == 0 ? true : false;
        }
    }
    /*
       Flow Has methods such as the following that just wrap method execution in a try catch and use the handler passed in to handle the exception

        public static async Task<Flow<T>> TryToFlow<T>(Func<Task<Flow<T>>> operationToTry, Func<Exception, Flow<T>> exceptionHandler)
        {
            try
            {
                return await operationToTry();
            }
            catch (Exception ex) { return exceptionHandler(ex); }
        }
    */
}
