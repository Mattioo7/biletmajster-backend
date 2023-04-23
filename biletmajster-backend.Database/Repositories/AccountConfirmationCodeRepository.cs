using biletmajster_backend.Database.Entities;
using biletmajster_backend.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace biletmajster_backend.Database.Repositories;

public class AccountConfirmationCodeRepository : BaseRepository<AccountConfirmationCode>,
    IAccountConfirmationCodeRepository
{
    public AccountConfirmationCodeRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task UpdateOrganizerConfirmationCodeAsync(Organizer organizer, string code)
    {
        var confirmationCode = await DbSet.FirstOrDefaultAsync(x => x.Organizer.Id == organizer.Id);

        if (confirmationCode == null)
        {
            confirmationCode = new AccountConfirmationCode
            {
                Organizer = organizer,
                Code = code,
                CreatedAt = DateTime.Now
            };
            await DbSet.AddAsync(confirmationCode);
        }
        else
        {
            confirmationCode.Code = code;
            DbSet.Update(confirmationCode);
        }

        await mDbContext.SaveChangesAsync();
    }

    public async Task<List<AccountConfirmationCode>> GetConfirmationCodesForOrganizerAsync(Organizer organizer)
    {
        return await DbSet.Where(c => c.Organizer.Id == organizer.Id).ToListAsync();
    }

    protected override DbSet<AccountConfirmationCode> DbSet => mDbContext.AccountConfirmationCodes;
}