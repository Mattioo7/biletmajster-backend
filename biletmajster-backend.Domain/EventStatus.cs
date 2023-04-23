using System.Runtime.Serialization;

namespace biletmajster_backend.Domain;

public enum EventStatus
{
    [EnumMember(Value = "inFuture")] InFuture = 0,
    [EnumMember(Value = "pending")] Pending = 1,
    [EnumMember(Value = "done")] Done = 2,
    [EnumMember(Value = "cancelled")] Cancelled = 3
}