using biletmajster_backend.Database.Entities;

namespace biletmajster_backend.Database.Repositories.Interfaces;

public interface IAccountConfirmationCodeRepository
{
    public Task UpdateOrganizerConfirmationCodeAsync(Organizer organizer, string code);
    
    public Task<List<AccountConfirmationCode>> GetConfirmationCodesForOrganizerAsync(Organizer organizer);
}