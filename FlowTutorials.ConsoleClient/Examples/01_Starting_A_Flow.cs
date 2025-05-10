using Flow.Core.Areas.Extensions;
using Flow.Core.Areas.Returns;
using FlowTutorials.ConsoleClient.Common.Seeds;
using FlowTutorials.ConsoleClient.Common.Utilities;

namespace FlowTutorials.ConsoleClient.Examples;

internal class Starting_A_Flow() : IFlowExample
{
    public Range  PrintLineRange  => 19 .. 73;
    public string FileName        => "01_Starting_A_Flow.cs";
    public int    Order           => 1;
    public string Name            => "Starting a flow";
    public string Description     => "Shows how to start a flow either explicitly, implicitly, using the factory methods or extension methods";

    public async Task RunExample()
    
        => await Getting_Values_In_And_Out_Of_Flow();

    private static async Task Getting_Values_In_And_Out_Of_Flow()
    {
        /*
            * Explicit creation of a flow with the factory methods, using the implicit operators or extension methods
            * All built-in failures are accessed via the Failure class (e.g., Failure.ValidationFailure), unless you use a static using to drop the prefix."
        */

        var explicitFailureFlow = Flow<int>.Failed(new Failure.ApplicationFailure("Explicitly created failure"));
        var explicitSuccessFlow = Flow<int>.Success(42);

        Flow<int> implicitFailedFlow  = new Failure.ApplicationFailure("Implicitly created failure");
        Flow<int> implicitSuccessFlow = 84;

        static int NonFlowReturningLocalMethod() => 42;

        static Flow<string> FlowReturningLocalMethod(string value="") => value + " The meaning of life, the universe, and everything else.";

        /*
            * Chain a non-flow returning method or value to a flow returning method using the Then extension, with or without changing the type and explicit lambda 
        */

        var chainValueToFlowMethod = "42 is the answer to:".Then(FlowReturningLocalMethod);

        var chainMethodToFlowMethod = NonFlowReturningLocalMethod().Then(value => FlowReturningLocalMethod(value.ToString()));

        /*
            * The above should be easy enough to follow, but you need to get your head around getting values out of a Flow<T> or most other result types.
            * When ever you need the final value you have to provide functions for two cases, failure or success, but don't worry, these can be really simple.
            * You just have to think about what you want to do if its a failure case, as usually getting a success value out is just a lambda such as success => success.
            * 
            * The method you use to get a values from a result type is usually called Match. Flow has this, but also has extension methods that use Match such as Finally
        */

        GeneralUtils.WriteLine($"Explicit failure flow, IsFailure: {explicitFailureFlow.IsFailure}", ConsoleColor.Red);
        GeneralUtils.WriteLine($"Explicit failure flow outputs using Match: {explicitFailureFlow.Match(failure => 0, success => success)}", ConsoleColor.Red);
        GeneralUtils.WriteLine($"Explicit failure flow outputs using Finally: {explicitFailureFlow.Finally(failure => 0, success => success)}\r\n", ConsoleColor.Red);

        GeneralUtils.WriteLine($"Explicit success flow, IsSuccess:: {explicitSuccessFlow.IsSuccess}", ConsoleColor.Green);
        GeneralUtils.WriteLine($"Explicit success flow outputs using Match: {explicitSuccessFlow.Match(failure => 0, success => success)}", ConsoleColor.Green);
        GeneralUtils.WriteLine($"Explicit success flow outputs using Finally: {explicitSuccessFlow.Finally(failure => 0, success => success)}\r\n", ConsoleColor.Green);
        
        /*
            * I know this is a failure, which you usually don't, so I can switch the success to match the type output by the failure.Reason. 
        */
        
        GeneralUtils.WriteLine($"Implicit failure flow outputs: {implicitFailedFlow.Finally(failure => failure.Reason, success => "Not a failure")}",ConsoleColor.Red);
        GeneralUtils.WriteLine($"Implicit success flow outputs: {implicitSuccessFlow.Finally(failure => 0, success => success)}\r\n",ConsoleColor.Green);

        GeneralUtils.WriteLine($"Chain Value To Flow Method outputs: {chainValueToFlowMethod.Finally(failure => failure.Reason, success => success)}",ConsoleColor.Green);
        GeneralUtils.WriteLine($"Chain Method To Flow Method outputs: {chainMethodToFlowMethod.Finally(failure => failure.Reason, success => success)}\r\n", ConsoleColor.Green);

        await Task.CompletedTask;
    }
}
