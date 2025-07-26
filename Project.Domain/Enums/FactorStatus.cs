using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Project.Domain.Enums
{
    public enum FactorStatus
    {
        [EnumMember(Value = "در انتظار")]
        [Display(Name = "در انتظار")]
        Pending,

        [EnumMember(Value = "تکمیل شده")]
        [Display(Name = "تکمیل شده")]
        Completed,

        [EnumMember(Value = "خطا")]
        [Display(Name = "خطا")]
        Error,
    }
}
