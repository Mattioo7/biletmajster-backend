namespace biletmajster_backend.Interfaces;

public interface ICustomMailService
{
    public Task SendMailAsync(string recipient, string recipientMail, string subject, string body, bool useHtml = false, CancellationToken ct = default);
}