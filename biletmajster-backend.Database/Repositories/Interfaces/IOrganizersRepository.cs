using biletmajster_backend.Database.Entities;

namespace biletmajster_backend.Database.Repositories.Interfaces;

public interface IOrganizersRepository
{
    public Task<Organizer?> GetOrganizerByIdAsync(long id);
    public Task<Organizer?> GetOrganizerByNameAsync(string name);
    public Task<Organizer?> GetOrganizerByEmailAsync(string email);
    public Task<Organizer> CreateOrganizerAsync(string name, string email, byte[] passwordHash, byte[] passwordSalt);
    public Task UpdateOrganizerAccountStatusAsync(Organizer organizer, OrganizerAccountStatus status);
    public Task DeleteOrganizerByIdAsync(long id);

    public Task SaveChangesAsync();
    public Organizer UpdateOrganizer(Organizer organizerToUpdate);
}