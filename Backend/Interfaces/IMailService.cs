namespace Backend.Interfaces;

public interface ICustomMailService
{
    public Task SendMailAsync(string Recipient, string RecipientMail, string Subject, string Body, bool UseHtml = false, CancellationToken ct = default);
}