using biletmajster_backend.Database.Entities;

namespace biletmajster_backend.Interfaces;

public interface IConfirmationService
{
    public Task SendConfirmationRequestAsync(Organizer organizer);
    public Task<string> GetConfirmationCodeAsync(Organizer organizer);
}