using Flow.Core.Areas.Extensions;
using Flow.Core.Areas.Returns;
using Flow.Core.Common.Models;
using FlowTutorials.ConsoleClient.Common.Models;
using FlowTutorials.ConsoleClient.Common.Seeds;
using FlowTutorials.ConsoleClient.Common.Utilities;

namespace FlowTutorials.ConsoleClient.Examples;

internal class Flow_Extension_Actions : IFlowExample
{
    public Range  PrintLineRange => 21 .. 82;
    public string FileName       => "02_Flow_Extension_Actions.cs";
    public int    Order          => 2;
    public string Name           => "Flow extension actions";
    public string Description    => "Shows how to use the extension methods that allow you to Tap/Tee actions during a flow for side effects such as logging";

    public async Task RunExample()
    
        => await Using_Actions_For_Side_Effect_Mid_Flow();
    
    public static async Task Using_Actions_For_Side_Effect_Mid_Flow()
    {
        /*
             * Some times you may want to do something mid flow, (Tap) which is non returning and is not intended to alter the flow, such as sending an email or logging and entry.
             * Flow has overloaded extension methods that allow you to perform sync or async actions mid flow.
             * 
             * You can organise flows to suit, In the example below if the database call fails the OnSuccess is skipped and the OnFailure will log the reason using a sync action.
             * If the database call is successful the SendEmail async method is called. Finally if database call was successful the Contact will be returned, otherwise, in this instance a null is returned.
             * 
             * You do not have to just chain everything, for example you could stop mid flow, use flowResultPass.IsSuccess in an if block and decide what to do. I tend to just chain everything together and
             * use a finally. For example, I would just check the final result if(flowResultPass is null) ... if I had not made a compensating flow ect.
        */

        var flowResultPass = await GetContactFromDatabase(42)
                                    .OnSuccess(contact => SendEmail(contact.EmailAddress))
                                        .OnFailure(failure => Console.WriteLine(failure.Reason)) // < < < As we will see in later demo, we could call another flow returning method here instead.
                                            .Finally(failure => (Contact?)null, success => success);

        GeneralUtils.WriteLine($"flowResultPass returned: {flowResultPass}\r\n", ConsoleColor.Green);

        var flowResultFail = await GetContactFromDatabase(999)
                                      .OnSuccess(contact => SendEmail(contact.EmailAddress))
                                          .OnFailure(failure => Console.WriteLine(failure.Reason))
                                              .Finally(failure => (Contact?)null, success => success);

        GeneralUtils.WriteLine($"flowResultFail returned: {flowResultFail?.ToString() ?? "null\r\n"}", ConsoleColor.Red);

        /*
            * May be your thinking but what if the SendEmail fails and I want to handle that mid flow but want the Contact at the end if the database call is successful.
            * No problem, lets use a Flow returning SendEmail message that has no value to return, so a unit type would suffice, Flow has a type called None for this.
        */ 

        var nestedFlow = await GetContactFromDatabase(42)
                                    .OnSuccess(async contact => 
                                    {
                                        _ = await SendEmail(contact.EmailAddress, true)
                                                    .OnFailure(failure => GeneralUtils.WriteLine(failure.Reason, ConsoleColor.Red));
                                    }
                                     ).OnFailure(failure => Console.WriteLine(failure.Reason))
                                        .Finally(failure => (Contact?)null, success => success);

        GeneralUtils.WriteLine($"nestedFlow returned: {nestedFlow}\r\n",ConsoleColor.Green);
    }

    public static Task<Flow<Contact>> GetContactFromDatabase(int contactID) 
       
        =>  (contactID == 42) ? Task.FromResult(Flow<Contact>.Success(new Contact(42, "John", "Doe", "john.doe@flow.com"))) : Task.FromResult(Flow<Contact>.Failed(new Failure.DatabaseFailure("Entry not found"))); 
      
    public static Task SendEmail(string emailAddress)
    
        =>  Console.Out.WriteLineAsync($"Sending email to: {emailAddress}");

    public static Task<Flow<None>> SendEmail(string emailAddress, bool fail = true)
    {
        if (true == fail) return Task.FromResult(Flow<None>.Failed(new Failure.MessagingFailure("Bad email address")));

        Console.Out.WriteLineAsync($"Sending email to: {emailAddress}");

        return Task.FromResult(Flow<None>.Success());
    }
}
