using FlowTutorials.ConsoleClient.Common.Seeds;

namespace FlowTutorials.ConsoleClient.Common.Models;


public record class Contact(int ContactID, string FirstName, string Surname, string EmailAddress);

public record class UpdateContactCommand(int ContactID, string FirstName, string Surname, string EmailAddress) : ICommand;

public record class ContactView(int ContactID, string FullName, string Mobile);

public record ValidationResult(bool IsValid, List<FieldError> FieldErrors);

public record FieldError(string Field, string Message);

internal class Registration
{
    public required DateTime RegistrationDate { get; set; }
    public required string FirstName { get; set; }
    public required string Surname { get; set; }
    public required int Age { get; set; }
    public required string EmailAddress { get; set; }
    public required string AddressLine { get; set; }
    public required string Town { get; set; }
    public required string City { get; set; }
    public required string County { get; set; }
    public required string PostCode { get; set; }
}