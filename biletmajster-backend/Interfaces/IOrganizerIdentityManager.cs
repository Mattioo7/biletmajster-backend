using biletmajster_backend.Database.Entities;
using biletmajster_backend.Domain.DTOS;

namespace biletmajster_backend.Interfaces;

public interface IOrganizerIdentityManager
{
    public Task<Organizer> RegisterOrganizerAsync(string name, string email, string password);
    public Task<string> LoginAsync(string email, string password);
    public Task<Organizer> PatchOrganizerAsync(OrganizerDTO newOrganizer);
}