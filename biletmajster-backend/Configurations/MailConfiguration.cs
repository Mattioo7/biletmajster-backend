namespace Backend.Configurations;

public record MailConfiguration(string Host, string Port, bool UseSsl, string User, string Password, string Sender, string SenderMail);
