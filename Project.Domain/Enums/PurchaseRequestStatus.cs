using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Project.Domain.Enums
{
    public enum PurchaseRequestStatus
    {
        [Display(Name = "در انتظار تایید")] //this
        PaymentCompeleted_WaitForAdminConfirmation,
        [EnumMember(Value = "در حال آماده سازی")]
        [Display(Name = "در حال آماده سازی")]
        AcceptedByAdmin_CakeIsBeingBaked,
        [Display(Name = "در حال ارسال")] //this
        preparing,
        [Display(Name = "تکمیل سفارش")] //this
        Delivered,
        [EnumMember(Value = "لغو سفارش")]
        [Display(Name = "لغو سفارش")]
        Cancelled,
        [EnumMember(Value = "لغو سفارش توسط ادمین")]
        [Display(Name = "لغو سفارش توسط ادمین")]
        CancelledByAdmin,
        [EnumMember(Value = "ناموفق")]
        [Display(Name = "ناموفق")]
        NoPayment,
        [EnumMember(Value = "آرشیو")]
        [Display(Name = "آرشیو")]
        Archived
    }
}
