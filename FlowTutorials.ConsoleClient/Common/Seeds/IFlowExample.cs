namespace FlowTutorials.ConsoleClient.Common.Seeds;

public interface IFlowExample
{
    Range  PrintLineRange { get; }
    string FileName       { get; }
    string Name           { get; }
    string Description    { get; }
    int     Order         { get; }
    Task RunExample();
}
