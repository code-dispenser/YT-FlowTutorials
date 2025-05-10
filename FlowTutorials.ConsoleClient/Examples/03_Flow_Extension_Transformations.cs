using Flow.Core.Areas.Extensions;
using Flow.Core.Areas.Returns;
using Flow.Core.Common.Models;
using FlowTutorials.ConsoleClient.Common.Models;
using FlowTutorials.ConsoleClient.Common.Seeds;
using FlowTutorials.ConsoleClient.Common.Utilities;
using When.Core.Extensions;

namespace FlowTutorials.ConsoleClient.Examples
{
    internal class Flow_Extension_Transformations : IFlowExample
    {
        public Range PrintLineRange => 22..71;
        public string FileName => "03_Flow_Extension_Transformations.cs";
        public int Order => 3;
        public string Name => "Flow extension transformations";
        public string Description => "Shows how to use extension methods that transform flow results";

        public async Task RunExample()

            => await Flow_Transformation_And_Chaining();

        public async Task Flow_Transformation_And_Chaining()
        {
            var contact = new Contact(42, "John", "Doe", "john.doe@flow.com");
            var maxRun  = 5;

            for(int index = 1; index <= maxRun; index++)
            {
                Console.WriteLine($"Run {index} of {maxRun} produced:");

                var flowResult = await SomeController_UpdateContact(contact)
                                            .Finally(failure => { ShowFailureDialog(failure); return (ContactView?)null; }, success => success);

                (flowResult is not null).WhenTrue(() => GeneralUtils.WriteLine($"Output: {flowResult}\r\n",ConsoleColor.Green));
            }
        }

        public static async Task<Flow<ContactView>> SomeController_UpdateContact(Contact contact)
        
            => await SomeHandler_HandleUpdateContact(new UpdateContactCommand(contact.ContactID, contact.FirstName, contact.Surname, contact.EmailAddress))
                                    .OnSuccess(_ => SomeHandler_HandlerGetContactView(contact.ContactID));
    
        public static async Task<Flow<None>> SomeHandler_HandleUpdateContact(UpdateContactCommand contactCommand)
        
            => await CommandValidator(contactCommand)
                        .OnSuccess(async _ =>
                                {
                                    return (await CauseRandomFailure()) ? new Failure.DatabaseFailure("Update failed. The requested contact is no longer in the system.") : Flow<None>.Success();
                                });

        public static async Task<Flow<bool>> CommandValidator(ICommand command)
            
            => (await CauseRandomFailure()) ? new Failure.ValidationFailure("Validation errors", new() {{ "FirstName", "Invalid format" } }) : true;
        
        public static async Task<Flow<ContactView>> SomeHandler_HandlerGetContactView(int contactID)

            => (await CauseRandomFailure()) ? new Failure.DatabaseFailure($"The requested Contact with the ID: {contactID} was not found. This entry may have been deleted.")
                                            : new ContactView(contactID, "John Doe", "+441234567890");
        
        public static void ShowFailureDialog(Failure failure)
        {
            string message = failure is Failure.ValidationFailure ? $"{failure.Reason}: {String.Join("\r\n", failure.Details.Select((k) => $"{k.Key}: {k.Value}"))}" : failure.Reason;
            GeneralUtils.WriteLine($"Output: {message}\r\n", ConsoleColor.Red);
        }
        public static async Task<bool> CauseRandomFailure()
        {
            await Task.Delay(1);
            return Random.Shared.Next(1,100) % 5 == 0 ? true: false;
        }
    }
}
