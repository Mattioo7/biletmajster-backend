using MailKit.Net.Smtp;
using MimeKit;
using biletmajster_backend.Interfaces;
using biletmajster_backend.Configurations;

namespace biletmajster_backend.Services;

public class MailService : ICustomMailService
{
    private MailConfiguration cfg;
    private int port;
    private bool useSsl;
    
    private readonly Logger<MailService> _logger;

    public MailService(MailConfiguration config, Logger<MailService> logger)
    {
        cfg = config;
        port = int.Parse(cfg.Port);
        useSsl = cfg.UseSsl;
        _logger = logger;
    }

    public virtual async Task SendMailAsync(string Recipient, string RecipientMail, string Subject, string Body, bool UseHtml = false, CancellationToken ct = default)
    {
        _logger.LogDebug($"Sending email to {RecipientMail}");
        
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(cfg.Sender, cfg.SenderMail));
        message.To.Add(new MailboxAddress(Recipient, RecipientMail));
        message.Subject = Subject;

        message.Body = new TextPart(UseHtml ? "html" : "plain") { Text = Body };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(cfg.Host, port, useSsl, ct);
            await client.AuthenticateAsync(cfg.User, cfg.Password, ct);
            await client.SendAsync(message, ct);
            await client.DisconnectAsync(true, ct);
        }
    }
}