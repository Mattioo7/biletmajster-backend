using biletmajster_backend.Contracts;
using biletmajster_backend.Domain;

namespace biletmajster_backend.Interfaces;

public interface IOrganizerIdentityManager
{
    public Task<Organizer> RegisterOrganizerAsync(string name, string email, string password);
    public Task<string> LoginAsync(string email, string password);
    public Task<Organizer> PatchOrganizerAsync(long organizerId, OrganizerPatchDto newOrganizer);
}