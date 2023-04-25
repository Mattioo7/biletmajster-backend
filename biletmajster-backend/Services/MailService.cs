using MailKit.Net.Smtp;
using MimeKit;
using biletmajster_backend.Interfaces;
using biletmajster_backend.Configurations;

namespace biletmajster_backend.Services;

public class MailService : ICustomMailService
{
    private MailConfiguration _cfg;
    private int _port;
    private bool _useSsl;
    
    private readonly ILogger<MailService> _logger;

    public MailService(MailConfiguration config, ILogger<MailService> logger)
    {
        _cfg = config;
        _port = int.Parse(_cfg.Port);
        _useSsl = _cfg.UseSsl;
        _logger = logger;
    }

    public virtual async Task SendMailAsync(string recipient, string recipientMail, string subject, string body, bool useHtml = false, CancellationToken ct = default)
    {
        _logger.LogDebug($"Sending email to {recipientMail}");
        
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_cfg.Sender, _cfg.SenderMail));
        message.To.Add(new MailboxAddress(recipient, recipientMail));
        message.Subject = subject;

        message.Body = new TextPart(useHtml ? "html" : "plain") { Text = body };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_cfg.Host, _port, _useSsl, ct);
            await client.AuthenticateAsync(_cfg.User, _cfg.Password, ct);
            await client.SendAsync(message, ct);
            await client.DisconnectAsync(true, ct);
        }
    }
}