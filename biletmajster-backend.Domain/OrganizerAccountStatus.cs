namespace biletmajster_backend.Database.Entities;

public enum OrganizerAccountStatus
{
    Created = 0,
    PendingForConfirmation = 1,
    Confirmed = 2,
    Deleted = 3
}