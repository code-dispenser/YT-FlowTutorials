using FlowTutorials.ConsoleClient.Common.Seeds;

namespace FlowTutorials.ConsoleClient.Common.Models;


public record class Contact(int ContactID, string FirstName, string Surname, string EmailAddress);

public record class UpdateContactCommand(int ContactID, string FirstName, string Surname, string EmailAddress) : ICommand;

public record class ContactView(int ContactID, string FullName, string Mobile);

public record ValidationResult(bool IsValid, List<FieldError> FieldErrors);

public record FieldError(string Field, string Message);

internal record class Registration
{
    public required DateTime RegistrationDate { get; init; }
    public required string   FirstName        { get; init; }
    public required string  Surname           { get; init; }
    public required int     Age               { get; init; }
    public required string  EmailAddress      { get; init; }
    public required string  AddressLine       { get; init; }

}
