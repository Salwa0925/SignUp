namespace UserApi;

public interface IEmailService
{
    Task SendConfirmationEmail(string email, string confirmationToken);
}
