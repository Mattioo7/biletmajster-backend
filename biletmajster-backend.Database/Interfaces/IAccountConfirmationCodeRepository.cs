using biletmajster_backend.Domain;

namespace biletmajster_backend.Database.Interfaces;

public interface IAccountConfirmationCodeRepository
{
    public Task UpdateOrganizerConfirmationCodeAsync(Organizer organizer, string code);
    
    public Task<List<AccountConfirmationCode>> GetConfirmationCodesForOrganizerAsync(Organizer organizer);
}