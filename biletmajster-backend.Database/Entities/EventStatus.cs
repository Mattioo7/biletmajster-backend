using System.Runtime.Serialization;

namespace biletmajster_backend.Database.Entities;

public enum EventStatus
{
    [EnumMember(Value = "inFuture")] InFutureEnum = 0,
    [EnumMember(Value = "pending")] PendingEnum = 1,
    [EnumMember(Value = "done")] DoneEnum = 2,
    [EnumMember(Value = "cancelled")] CancelledEnum = 3
}