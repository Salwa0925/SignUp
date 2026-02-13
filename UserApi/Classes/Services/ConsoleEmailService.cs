namespace UserApi;
// Dette er bare en placeholder
// Printer bare ut link i console, ikke linket noe "ekte" service.
// Skal byttes litt ved implementasjon av ekte service.
public class ConsoleEmailService : IEmailService
{
    public Task SendConfirmationEmail(string email, string confirmationToken)
    {
        var confirmationLink = $"https://localhost:5001/api/auth/confirm-email?token={confirmationToken}";

        Console.WriteLine("=======<<<======================>>>==========");
        Console.WriteLine($"  Confirmation email for: {email}");
        Console.WriteLine($"  Click here: {confirmationLink}");
        Console.WriteLine("======<<<========================>>>==========");

        return Task.CompletedTask;
    }
}
