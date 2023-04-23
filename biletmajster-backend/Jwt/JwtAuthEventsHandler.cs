using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace biletmajster_backend.Jwt;


public class JwtAuthEventsHandler : JwtBearerEvents
{
    private const string BearerPrefix = "Bearer ";
    private const string ActualTokenPrefix = "sessionToken";

    private JwtAuthEventsHandler() => OnMessageReceived = MessageReceivedHandler;
    
    public static JwtAuthEventsHandler Instance { get; } = new();

    private Task MessageReceivedHandler(MessageReceivedContext context)
    {
        if (context.Request.Headers.TryGetValue(ActualTokenPrefix, out var headerValue))
        {
            string token = headerValue;
            if (!string.IsNullOrEmpty(token) && token.StartsWith(BearerPrefix))
            {
                token = token.Substring(BearerPrefix.Length);
            }

            context.Token = token;
        }

        return Task.CompletedTask;
    }
}