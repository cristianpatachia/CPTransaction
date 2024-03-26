using System.ComponentModel.DataAnnotations;
using CPServer.Domain.Resources;

namespace CPServer.Domain.ValueObjects.Enums
{
    public enum TransactionStatus
    {
        [Display(Order = (int)Pending, Name = "_0", ResourceType = typeof(TransactionStatusResrouces))]
        Pending = 0,

        [Display(Order = (int)Processing, Name = "_1", ResourceType = typeof(TransactionStatusResrouces))]
        Processing = 1,

        [Display(Order = (int)Failed, Name = "_2", ResourceType = typeof(TransactionStatusResrouces))]
        Failed = 2,

        [Display(Order = (int)Cancelled, Name = "_3", ResourceType = typeof(TransactionStatusResrouces))]
        Cancelled = 3,

        [Display(Order = (int)Completed, Name = "_4", ResourceType = typeof(TransactionStatusResrouces))]
        Completed = 4,
    }
}
