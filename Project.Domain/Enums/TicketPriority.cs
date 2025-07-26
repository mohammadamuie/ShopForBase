using System.Runtime.Serialization;

namespace Project.Domain.Enums
{
    public enum TicketPriority
    {
        [EnumMember(Value = "فوری")]
        Immediate,
        [EnumMember(Value = "معمولی")]
        Ordinary,
        [EnumMember(Value = "جهت اطلاع")]
        JustForInformation
    }

}
