using System.Runtime.Serialization;

namespace Project.Domain.Enums
{
    public enum TerminalStatus
    {
        [EnumMember(Value = "فعال")]
        Active,
        [EnumMember(Value = "غیرفعال")]
        Deactive,
        [EnumMember(Value = "از رده خارج")]
        OutOfOrder,
    }
}
