using System.Runtime.Serialization;

namespace Project.Domain.Enums
{
    public enum StoreStatus
    {
        [EnumMember(Value = "فعال")]
        Active,
        [EnumMember(Value = "غیرفعال")]
        Deactive,
        [EnumMember(Value = "قطع همکاری")]
        Terminated,
    }
}
