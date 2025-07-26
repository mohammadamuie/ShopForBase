using System.Runtime.Serialization;
using System.Xml.Linq;

namespace Project.Domain.Enums
{
    public enum UserStatus
    {
        [EnumMember(Value = "فعال")]
        Active,
        [EnumMember(Value = "لغو دسترسی")]
        Banned,
    }
}
