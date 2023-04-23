using biletmajster_backend.Domain;

namespace biletmajster_backend.Interfaces;

public interface IConfirmationService
{
    public Task SendConfirmationRequestAsync(Organizer organizer);
    public Task<string> GetConfirmationCodeAsync(Organizer organizer);
}