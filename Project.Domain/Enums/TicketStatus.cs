using System.Runtime.Serialization;

namespace Project.Domain.Enums
{
    public enum TicketStatus
    {
        [EnumMember(Value = "بسته شده")]
        Closed,
        [EnumMember(Value = "منتظر پاسخ کاربر")]
        WaitForUserResponse,
        [EnumMember(Value = "منتظر پاسخ پشتیبان")]
        WaitForAdminResponse,
        [EnumMember(Value = "درانتظار")]
        Pending,
    }
}
