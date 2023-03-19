using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Backend.Configurations;

namespace Backend.Services;

public class MailService
{
    private MailConfiguration cfg;
    private int port;
    private bool useSsl;

    public MailService(MailConfiguration config)
    {
        cfg = config;
        port = int.Parse(cfg.Port);
        useSsl = cfg.UseSsl;
    }

    public virtual async Task SendMailAsync(string Recipient, string RecipientMail, string Subject, string Body, bool UseHtml = false, CancellationToken ct = default)
    {
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