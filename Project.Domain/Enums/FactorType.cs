using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Project.Domain.Enums
{
    public enum FactorType
    {
        [EnumMember(Value = "خرید اشتراک")]
        [Display(Name = "خرید اشتراک")]
        Subscription,
    }
}
