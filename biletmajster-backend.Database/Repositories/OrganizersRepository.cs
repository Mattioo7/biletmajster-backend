using biletmajster_backend.Domain;
using biletmajster_backend.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace biletmajster_backend.Database.Repositories;

public class OrganizersRepository : BaseRepository<Organizer>, IOrganizersRepository
{
    private readonly IConfiguration _configuration;

    public OrganizersRepository(ApplicationDbContext dbContext, IConfiguration configuration) : base(dbContext)
    {
        _configuration = configuration;
    }

    public async Task<Organizer?> GetOrganizerByIdAsync(long id)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Organizer?> GetOrganizerByNameAsync(string name)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Organizer?> GetOrganizerByEmailAsync(string email)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<Organizer> CreateOrganizerAsync(string name, string email, byte[] passwordHash,
        byte[] passwordSalt)
    {
        var organizer = new Organizer
        {
            Name = name,
            Email = email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Status = OrganizerAccountStatus.Pending
        };

        await DbSet.AddAsync(organizer);
        await SaveChangesAsync();

        return organizer;
    }

    public async Task UpdateOrganizerAccountStatusAsync(Organizer organizer, OrganizerAccountStatus status)
    {
        var organizerToUpdate = DbSet.FirstOrDefault(x => x.Id == organizer.Id);

        organizerToUpdate.Status = status;
        DbSet.Update(organizerToUpdate);

        await SaveChangesAsync();
    }

    public async Task DeleteOrganizerByIdAsync(long id)
    {
        var organizer = await DbSet.FindAsync(id);

        if (organizer != null)
        {
            DbSet.Remove(organizer);
        }

        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await MDbContext.SaveChangesAsync();
    }

    public async Task<Organizer> UpdateOrganizer(Organizer organizerToUpdate)
    {
        var ret =  DbSet.Update(organizerToUpdate).Entity;
        await SaveChangesAsync();
        return ret;
    }

    protected override DbSet<Organizer> DbSet => MDbContext.Organizers;
}